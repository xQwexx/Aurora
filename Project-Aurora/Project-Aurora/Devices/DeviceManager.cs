﻿using CSScriptLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Devices
{
    public class DeviceContainer
    {
        public IDevice Device { get; set; }

        public BackgroundWorker Worker = new BackgroundWorker();
        public Thread UpdateThread { get; set; } = null;

        private Tuple<DeviceColorComposition, bool> currentComp = null;
        private bool newFrame = false;

        public readonly object actionLock = new object();

        public DeviceContainer(IDevice device)
        {
            this.Device = device;
            Worker.DoWork += WorkerOnDoWork;
            Worker.RunWorkerCompleted += (sender, args) =>
            {
                lock (Worker)
                {
                    if (newFrame && !Worker.IsBusy)
                        Worker.RunWorkerAsync();
                }
            };
            //Worker.WorkerSupportsCancellation = true;
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            newFrame = false;
            lock(actionLock)
            {
                Device.UpdateDevice(currentComp.Item1, doWorkEventArgs,
                currentComp.Item2);
            }
        }

        public void UpdateDevice(DeviceColorComposition composition, bool forced = false)
        {
            newFrame = true;
            currentComp = new Tuple<DeviceColorComposition, bool>(composition, forced);
            lock (Worker)
            {
                if (Worker.IsBusy)
                    return;
                else
                    Worker.RunWorkerAsync();
            }
        }
    }
    public class DeviceManager
    {
        private const int RETRY_INTERVAL = 10000;
        private const int RETRY_ATTEMPTS = 5;
        private bool _InitializeOnceAllowed;
        private bool suspended;
        private bool resumed;
        private int _retryAttempts = RETRY_ATTEMPTS;
        public int RetryAttempts
        {
            get => _retryAttempts;
            private set
            {
                _retryAttempts = value;
                RetryAttemptsChanged?.Invoke(this, null);
            }
        }

        public List<DeviceContainer> DeviceContainers { get; } = new List<DeviceContainer>();
        public List<AuroraDeviceConnector> DeviceConnectors { get; } = new List<AuroraDeviceConnector>();
        public IEnumerable<AuroraDevice> IndividualDevices => DeviceConnectors.SelectMany(d => d.Devices);
        public IEnumerable<DeviceContainer> InitializedDeviceContainers => DeviceContainers.Where(d => d.Device.IsInitialized);

        public event EventHandler RetryAttemptsChanged;

        public DeviceManager()
        {
            AddDevicesFromAssembly();
            AddDevicesFromScripts();
            AddDevicesFromDlls();

            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
        }

        private void AddDevicesFromScripts()
        {
            string devices_scripts_path = System.IO.Path.Combine(Global.ExecutingDirectory, "Scripts", "Devices");

            if (!Directory.Exists(devices_scripts_path))
                return;

            Global.logger.Info("Loading devices from scripts...");

            foreach (string device_script in Directory.EnumerateFiles(devices_scripts_path, "*.*"))
            {
                try
                {
                    string ext = Path.GetExtension(device_script);
                    switch (ext)
                    {
                        case ".py":
                            var scope = Global.PythonEngine.ExecuteFile(device_script);
                            dynamic main_type;
                            if (scope.TryGetVariable("main", out main_type))
                            {
                                dynamic script = Global.PythonEngine.Operations.CreateInstance(main_type);

                                IDevice scripted_device = new Devices.ScriptedDevice.ScriptedDevice(script);

                                DeviceContainers.Add(new DeviceContainer(scripted_device));
                            }
                            else
                                Global.logger.Error("Script \"{0}\" does not contain a public 'main' class", device_script);

                            break;
                        case ".cs":
                            System.Reflection.Assembly script_assembly = CSScript.LoadFile(device_script);
                            foreach (Type typ in script_assembly.ExportedTypes)
                            {
                                dynamic script = Activator.CreateInstance(typ);

                                IDevice scripted_device = new Devices.ScriptedDevice.ScriptedDevice(script);

                                DeviceContainers.Add(new DeviceContainer(scripted_device));
                            }

                            break;
                        default:
                            Global.logger.Error("Script with path {0} has an unsupported type/ext! ({1})", device_script, ext);
                            break;
                    }
                }
                catch (Exception exc)
                {
                    Global.logger.Error("An error occured while trying to load script {0}. Exception: {1}", device_script, exc);
                }
            }
        }

        private void AddDevicesFromAssembly()
        {
            Global.logger.Info("Loading devices from assembly...");
            var deviceTypes = from type in Assembly.GetExecutingAssembly().GetTypes()
                              where typeof(IDevice).IsAssignableFrom(type)
                              && !type.IsAbstract
                              && type != typeof(ScriptedDevice.ScriptedDevice)
                              && type != typeof(OldAuroraDeviceWrapper)
                              let inst = (IDevice)Activator.CreateInstance(type)
                              orderby inst.DeviceName
                              select inst;

            foreach (var inst in deviceTypes)
            {
                DeviceContainers.Add(new DeviceContainer(inst));
            }
            var CorsairConnector = new Corsair.CorsairDeviceConnector();
            DeviceContainers.Add(new DeviceContainer(new OldAuroraDeviceWrapper(CorsairConnector)));
            DeviceConnectors.Add(CorsairConnector);
            var OpenRGBConnector = new OpenRGB.OpenRGBDeviceConnector();
            DeviceContainers.Add(new DeviceContainer(new OldAuroraDeviceWrapper(OpenRGBConnector)));
            DeviceConnectors.Add(OpenRGBConnector);
        }

        private void AddDevicesFromDlls()
        {
            string deviceDllFolder = Path.Combine(Global.AppDataDirectory, "Plugins", "Devices");

            if (!Directory.Exists(deviceDllFolder))
                return;

            Global.logger.Info("Loading devices from plugins");

            foreach (var deviceDll in Directory.EnumerateFiles(deviceDllFolder, "*.dll"))
            {
                try
                {
                    var deviceAssembly = Assembly.LoadFile(deviceDll);

                    foreach (var type in deviceAssembly.GetExportedTypes())
                    {
                        if (typeof(IDevice).IsAssignableFrom(type) && !type.IsAbstract)
                        {
                            IDevice devDll = (IDevice)Activator.CreateInstance(type);

                            DeviceContainers.Add(new DeviceContainer(devDll));
                        }
                    }
                }
                catch (Exception e)
                {
                    Global.logger.Error($"Error loading device dll: {deviceDll}. Exception: {e.Message}");
                }
            }
        }

        public void RegisterVariables()
        {
            foreach (var device in DeviceContainers)
            {
                Global.Configuration.VarRegistry.Combine(device.Device.RegisteredVariables);
            }
        }

        private void RetryAll()
        {
            while (RetryAttempts > 0)
            {
                foreach (var dc in DeviceContainers)
                {
                    if (dc.Device.IsInitialized || Global.Configuration.DevicesDisabled.Contains(dc.Device.GetType()))
                        continue;

                    lock(dc.actionLock)
                        dc.Device.Initialize();
                }
                RetryAttempts--;
                Thread.Sleep(RETRY_INTERVAL);
            }
        }

        public void InitializeOnce()
        {
            if (!InitializedDeviceContainers.Any() && _InitializeOnceAllowed)
            {
                InitializeDevices();
            }
        }

        public void InitializeDevices()
        {
            if (suspended)
                return;

            int devicesToRetry = 0;

            foreach (var dc in DeviceContainers)
            {
                if (dc.Device.IsInitialized || Global.Configuration.DevicesDisabled.Contains(dc.Device.GetType()))
                    continue;

                lock (dc.actionLock)
                {
                    if (!dc.Device.Initialize())
                        devicesToRetry++;
                }

                var s = $"[Device][{dc.Device.DeviceName}] ";

                if (dc.Device.IsInitialized)
                    s += "Initialized Successfully.";
                else
                    s += "Failed to initialize.";

                Global.logger.Info(s);
            }
            DeviceConnectors.ForEach(dc => dc.Initialize());

            if (devicesToRetry > 0)
                Task.Run(RetryAll);

            _InitializeOnceAllowed = InitializedDeviceContainers.Any();
        }

        public void ShutdownDevices()
        {
            foreach (var dc in InitializedDeviceContainers)
            {
                lock (dc.actionLock)
                    dc.Device.Shutdown();
                Global.logger.Info($"[Device][{dc.Device.DeviceName}] Shutdown");
            }
            DeviceConnectors.ForEach(dc => dc.Shutdown());
        }

        public void ResetDevices()
        {
            foreach (var dc in InitializedDeviceContainers)
            {
                lock (dc.actionLock)
                    dc.Device.Reset();
            }
            DeviceConnectors.ForEach(dc => dc.Reset());
        }

        public void RegisterViewPort(ref UniqueDeviceId devicId, int viewPort)
        {
            foreach (var dc in IndividualDevices)
            {
                if (dc.id?.ViewPort == viewPort)
                    dc.id.ViewPort = null;
            }
            devicId.ViewPort = viewPort;
            foreach (var dc in IndividualDevices)
            {
                if (dc.id == devicId)
                    dc.id = devicId;
            }
        }
        public void UpdateDevices(Dictionary<int, DeviceColorComposition> compositionList, bool forced = false)
        {
            foreach (var dc in InitializedDeviceContainers)
            {
                lock (dc.actionLock)
                {

                    foreach (var composition in compositionList)
                    {
                        if (!IndividualDevices.Where(dc => dc.id?.ViewPort == composition.Key).Any())
                            dc.UpdateDevice(composition.Value, forced);
                    }

                }
            }
            foreach (var item in compositionList)
            {
                var dc = IndividualDevices.Where(d => d.IsConnected() && d.id.ViewPort == item.Key);
                if (dc.Any())
                {
                    dc.First().UpdateDevice(item.Value);
                }
            }
        }

        #region SystemEvents
        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            Global.logger.Info($"SessionSwitch triggered with {e.Reason}");
            if (e.Reason.Equals(SessionSwitchReason.SessionUnlock) && (suspended || resumed))
            {
                Global.logger.Info("Resuming Devices -- Session Switch Session Unlock");
                suspended = false;
                resumed = false;
                InitializeDevices();
            }
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Suspend:
                    Global.logger.Info("Suspending Devices");
                    suspended = true;
                    this.ShutdownDevices();
                    break;
                case PowerModes.Resume:
                    Global.logger.Info("Resuming Devices -- PowerModes.Resume");
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    resumed = true;
                    suspended = false;
                    this.InitializeDevices();
                    break;
            }
        }
        #endregion
    }
}
