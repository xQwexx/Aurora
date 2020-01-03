using Aurora.Profiles;
using CSScriptLibrary;
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
        public IAuroraDevice Device { get; set; }
        private readonly Stopwatch Watch = new Stopwatch();
        private long LastUpdateTime = 0;
        private bool UpdateIsOngoing = false;
        public async void UpdateDevice(DeviceColorComposition composition)
        {
            if (Device.IsConnected())
            {
                if (Global.Configuration.devices_disabled.Contains(Device.GetType()))
                {
                    //Initialized when it's supposed to be disabled? SMACK IT!
                    Device.Disconnect();
                    return;
                }

                if (!UpdateIsOngoing)
                {
                    UpdateIsOngoing = true;
                    Watch.Restart();

                    await Task.Run(() => Device.UpdateDevice(composition));

                    Watch.Stop();
                    LastUpdateTime = Watch.ElapsedMilliseconds;

                    UpdateIsOngoing = false;
                }
    
            }
        }
        public virtual string GetDeviceUpdatePerformance()
        {
            return Device.IsConnected() ? LastUpdateTime + " ms" : "";
        }
        public DeviceContainer(IAuroraDevice device)
        {
            Device = device;
            Connect();
        }
        public bool Connect()
        {
            if (Device.GetDeviceType() == AuroraDeviceType.Keyboard && Global.Configuration.devices_disable_keyboard ||
                Device.GetDeviceType() == AuroraDeviceType.Mouse && Global.Configuration.devices_disable_mouse ||
                Device.GetDeviceType() == AuroraDeviceType.Headset && Global.Configuration.devices_disable_headset)
            {
                Device.Disconnect();
                return false;
            }
            else
            {
                return Device.Connect();
            }
        }
 
    }
    public class AuroraDeviceConnector
    {
        public IDeviceConnector DeviceConnector;
        public List<DeviceContainer> DeviceContainers { get; } = new List<DeviceContainer>();

        private event EventHandler NewSuccessfulInitiation;
        public AuroraDeviceConnector(IDeviceConnector deviceConnector, EventHandler newInitiation)
        {
            DeviceConnector = deviceConnector;
            
            NewSuccessfulInitiation += newInitiation;
        }
        public bool IsInitialized() => DeviceConnector.IsInitialized();

        public async void Initialize(bool forceRetry = false)
        {
            if (IsInitialized() || Global.Configuration.devices_disabled.Contains(DeviceConnector.GetType()))
                return;

            if (await Task.Run(() => DeviceConnector.Initialize()))
            {
                foreach (var device in DeviceConnector.GetDevices())
                {
                    DeviceContainers.Add(new DeviceContainer(device));
                }
                NewSuccessfulInitiation?.Invoke(this, new EventArgs());
            }
            Global.logger.Info("Device, " + DeviceConnector.GetDeviceName() + ", was" + (DeviceConnector.IsInitialized() ? "" : " not") + " initialized");
        }

        public void Reset()
        {
            if (IsInitialized())
            {
                DeviceConnector.Reset();
            }
        }
        public void Shutdown()
        {
            if (IsInitialized())
            {
                DeviceContainers.Clear();
                DeviceConnector.Shutdown();
                Global.logger.Info("Device, " + DeviceConnector.GetDeviceName() + ", was shutdown");
            }
        }
    }
    public class DeviceManager : IDisposable
    {
        public List<AuroraDeviceConnector> DeviceSdkContainers { get; } = new List<AuroraDeviceConnector>();
        public IEnumerable<DeviceContainer> DeviceContainers => DeviceSdkContainers.SelectMany(d => d.DeviceContainers);
        public IEnumerable<DeviceContainer> NotInitializedDeviceContainers => DeviceSdkContainers.Where(d => !d.IsInitialized()).SelectMany(dc => dc.DeviceConnector.GetDevices()).Select(d => new DeviceContainer(d)).ToList();
        public IEnumerable<IAuroraDevice> Devices => DeviceContainers.Select(d => d.Device);
        public IEnumerable<DeviceContainer> InitializedDeviceContainers => DeviceContainers.Where(dc => dc.Device.IsConnected());
        public IEnumerable<IAuroraDevice> InitializedDevices => Devices.Where(d => d.IsConnected());


        private const int retryInterval = 10000;
        private const int retryAttemps = 5;
        private int retryAttemptsLeft = retryAttemps;
        private Thread retryThread;
        private bool suspended = false;

        public int RetryAttempts
        {
            get
            {
                return retryAttemptsLeft;
            }
        }
        public event EventHandler NewDevicesInitialized;

        public DeviceManager()
        {
            //DeviceContainers.Add(new DeviceContainer(new Razer.RazerDevice()));
            //DeviceContainers.Add(new DeviceContainer(new Roccat.RoccatDevice()));
            //DeviceContainers.Add(new DeviceContainer(new Clevo.ClevoDevice()));
            //DeviceContainers.Add(new DeviceContainer(new AtmoOrbDevice.AtmoOrbDevice()));
            //DeviceContainers.Add(new DeviceContainer(new SteelSeries.SteelSeriesDevice()));
            //DeviceContainers.Add(new DeviceContainer(new UnifiedHID.UnifiedHIDDevice()));
            //DeviceContainers.Add(new DeviceContainer(new Creative.SoundBlasterXDevice()));
            //DeviceContainers.Add(new DeviceContainer(new LightFX.LightFxDevice()));
            //DeviceContainers.Add(new DeviceContainer(new YeeLight.YeeLightDevice()));
            //DeviceContainers.Add(new DeviceContainer(new Asus.AsusDevice()));
            //DeviceContainers.Add(new DeviceContainer(new NZXT.NZXTDevice()));

            string devices_scripts_path = System.IO.Path.Combine(Global.ExecutingDirectory, "Scripts", "Devices");

            if (Directory.Exists(devices_scripts_path))
            {
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

                                    IDeviceConnector scripted_device = new Devices.ScriptedDevice.ScriptedDevice(script);

                                    DeviceSdkContainers.Add(new AuroraDeviceConnector(scripted_device, NewDevicesInitialized));
                                }
                                else
                                    Global.logger.Error("Script \"{0}\" does not contain a public 'main' class", device_script);

                                break;
                            case ".cs":
                                System.Reflection.Assembly script_assembly = CSScript.LoadFile(device_script);
                                foreach (Type typ in script_assembly.ExportedTypes)
                                {
                                    dynamic script = Activator.CreateInstance(typ);

                                    IDeviceConnector scripted_device = new Devices.ScriptedDevice.ScriptedDevice(script);

                                    DeviceSdkContainers.Add(new AuroraDeviceConnector(scripted_device, NewDevicesInitialized));
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
            
            string deviceDllFolder = Path.Combine(Global.ExecutingDirectory, "Plugins", "Devices");

            Global.logger.Info("Loading Device Plugins");
            if (Directory.Exists(deviceDllFolder))
            {
                foreach (var deviceDll in Directory.EnumerateFiles(deviceDllFolder, "Device-*.dll"))
                {
                    try
                    {
                        var deviceAssembly = Assembly.LoadFrom(deviceDll);

                        foreach (var type in deviceAssembly.GetExportedTypes())
                        {
                            if (typeof(IDeviceConnector).IsAssignableFrom(type))
                            {
                                IDeviceConnector devDll = (IDeviceConnector)Activator.CreateInstance(type);

                                DeviceSdkContainers.Add(new AuroraDeviceConnector(devDll, NewDevicesInitialized));
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Global.logger.Error($"Error loading device dll: {deviceDll}. Exception: {e.Message}");
                    }
                }
            }

            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
        }

        bool resumed = false;
        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            Global.logger.Info($"SessionSwitch triggered with {e.Reason}");
            if (e.Reason.Equals(SessionSwitchReason.SessionUnlock) && (suspended || resumed))
            {
                Global.logger.Info("Resuming Devices -- Session Switch Session Unlock");
                suspended = false;
                resumed = false;
                this.Initialize(true);
            }
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Suspend:
                    Global.logger.Info("Suspending Devices");
                    suspended = true;
                    this.Shutdown();
                    break;
                case PowerModes.Resume:
                    Global.logger.Info("Resuming Devices -- PowerModes.Resume");
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    resumed = true;
                    suspended = false;
                    this.Initialize();
                    break;
            }
        }

        public void RegisterVariables()
        {
            //Register any variables
            foreach (var device in DeviceContainers)
                Global.Configuration.VarRegistry.Combine(device.Device.GetRegisteredVariables());
        }

        public void Initialize(bool forceRetry = false)
        {
            if (suspended)
                return;

            DeviceSdkContainers.ForEach(ds => ds.Initialize(forceRetry));


            if (DeviceSdkContainers.Where(dc => !dc.IsInitialized()).Any() && (retryThread == null || forceRetry || retryThread?.ThreadState == System.Threading.ThreadState.Stopped))
            {
                if (forceRetry)
                    retryThread?.Abort();
                retryThread = new Thread(RetryInitialize);
                retryThread.Start();
                return;
            }
        }

        private void RetryInitialize()
        {
            for (int try_count = 0; try_count < retryAttemps; try_count++)
            {
                if (suspended)
                    return;

                Global.logger.Info("Retrying Device Initialization");

                DeviceSdkContainers.ForEach(ds => ds.Initialize());
                retryAttemptsLeft--;

                //We don't need to continue the loop if we aren't trying to initialize anything
                if (DeviceSdkContainers.Where(dc => !dc.IsInitialized()).Any())
                    break;

                Thread.Sleep(retryInterval);
            }
        }

        public void InitializeOnce()
        {
            if (!AnyInitialized())
                Initialize();
        }

        public bool AnyInitialized() => DeviceSdkContainers.Where(d => d.IsInitialized()).Any();

        public void Shutdown() => DeviceSdkContainers.ForEach(d => d.Shutdown());

        public void ResetDevices() => DeviceSdkContainers.ForEach(d => d.Reset());

        public string GetDevices()
        {
            string devices_info = "";

            foreach (DeviceContainer device in DeviceContainers)
                devices_info += device.Device.GetDeviceDetails() + "\r\n";

            if (retryAttemptsLeft > 0)
                devices_info += "Retries: " + retryAttemptsLeft + "\r\n";

            return devices_info;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    Shutdown();
                    if (retryThread != null)
                    {
                        retryThread.Abort();
                        retryThread = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DeviceManager() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
