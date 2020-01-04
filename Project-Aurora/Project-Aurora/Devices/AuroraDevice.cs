using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Settings;

namespace Aurora.Devices
{
    public abstract class AuroraDevice
    {
        private readonly Stopwatch Watch = new Stopwatch();
        private long LastUpdateTime = 0;
        private bool UpdateIsOngoing = false;
        private bool DeviceIsConnected = false;

        private VariableRegistry variableRegistry;

        public event EventHandler ConnectionHandler;
        public event EventHandler UpdateFinished;
        /// <summary>
        /// Is called every frame (30fps). Update the device here
        /// </summary>
        public async void UpdateDevice(DeviceColorComposition composition)
        {
            if (IsConnected())
            {
                if (Global.Configuration.devices_disabled.Contains(GetType()))
                {
                    //Initialized when it's supposed to be disabled? SMACK IT!
                    Disconnect();
                    return;
                }

                if (!UpdateIsOngoing)
                {
                    UpdateIsOngoing = true;
                    Watch.Restart();

                    bool UpdateSuccess = await Task.Run(() => UpdateDeviceImpl(composition));

                    Watch.Stop();
                    LastUpdateTime = Watch.ElapsedMilliseconds;
                    if(!UpdateSuccess)
                    {
                        LogError(DeviceName + " device, error when updating device.");
                    }

                    UpdateFinished.Invoke(this, new EventArgs());
                    UpdateIsOngoing = false;
                }

            }
        }
        protected abstract bool UpdateDeviceImpl(DeviceColorComposition composition);

        public string GetDeviceUpdatePerformance()
        {
            return IsConnected() ? LastUpdateTime + " ms" : "";
        }
        public void Connect()
        {
            if (GetDeviceType() == AuroraDeviceType.Keyboard && Global.Configuration.devices_disable_keyboard ||
                GetDeviceType() == AuroraDeviceType.Mouse && Global.Configuration.devices_disable_mouse ||
                GetDeviceType() == AuroraDeviceType.Headset && Global.Configuration.devices_disable_headset)
            {
                Disconnect();
            }
            else
            {
                DeviceIsConnected = true;
                ConnectionHandler.Invoke(this, new EventArgs());
            }
        }

        public void Disconnect() 
        {
            if(IsConnected())
            {
                DeviceIsConnected = false;
                ConnectionHandler.Invoke(this, new EventArgs());
            }
        }

        protected abstract string DeviceName { get; }
        public string GetDeviceName() => DeviceName;

        public virtual string GetDeviceDetails() => DeviceName + ": " + (IsConnected() ? "Connected" : "Not connected");

        protected abstract AuroraDeviceType AuroraDeviceType { get; }
        public AuroraDeviceType GetDeviceType() => AuroraDeviceType;

        public VariableRegistry GetRegisteredVariables()
        {
            if (variableRegistry == null)
            {
                variableRegistry = new VariableRegistry();
                RegisterVariables(variableRegistry);
            }
            return variableRegistry;
        }
        /// <summary>
        /// Only called once when registering variables. Can be empty if not needed
        /// </summary>
        protected virtual void RegisterVariables(VariableRegistry local)
        {
            //purposefully empty, if varibles are needed, this should be overridden
        }

        public bool IsConnected() => DeviceIsConnected;

        protected void LogInfo(string s) => Global.logger.Info(s);

        protected void LogError(string s) => Global.logger.Error(s);

        protected Color CorrectAlpha(Color clr) => Utils.ColorUtils.CorrectWithAlpha(clr);

        protected VariableRegistry GlobalVarRegistry => Global.Configuration.VarRegistry;
    }

    public abstract class AuroraKeyboardDevice : AuroraDevice
    {
        protected override AuroraDeviceType AuroraDeviceType => AuroraDeviceType.Keyboard;
  
    }
 }
