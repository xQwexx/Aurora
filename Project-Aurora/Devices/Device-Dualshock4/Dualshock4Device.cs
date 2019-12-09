using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Devices;
using Aurora.Settings;
using DS4Windows;

namespace Device_Dualshock4
{
    public class Dualshock4Device : Device
    {
        protected override string DeviceName => "Dualshock 4";
        public int _battery;
        public double _latency;
        public bool _charging;
        private DS4HapticState _state;
        private DS4Color _initColor;
        private DS4Device _device;
        private DeviceKeys key;
        private Color _sendColor;

        public override string GetDeviceDetails()
        {
            string details = DeviceName;
            if (isInitialized)
            {
                details += ": Connected";

                switch (_device.getConnectionType())
                {
                    case ConnectionType.BT:
                        details += " over Bluetooth";
                        break;
                    case ConnectionType.USB:
                        details += " over USB";
                        break;
                    case ConnectionType.SONYWA:
                        details += " over DS4 Wireless adapter";
                        break;
                }

                details += _charging ? " ⚡" : " ";
                details += "🔋" + _battery + "% Delay: " + _latency.ToString("0.00") + " ms";
            }
            else
            {
                details += ": Not connected";
            }

            return details;
        }

        public override bool Initialize()
        {
            if (isInitialized)
                return true;

            key = GlobalVarRegistry.GetVariable<DeviceKeys>($"{DeviceName}_devicekey");
            DS4Devices.findControllers();
            IEnumerable<DS4Device> devices = DS4Devices.getDS4Controllers();

            if (!devices.Any())
                return false;

            _device = devices.ElementAt(0);
            _initColor = _device.LightBarColor;

            try
            {
                _device.Report += SendColor;
                _device.Report += UpdateProperties;
                _device.StartUpdate();
                isInitialized = true;
                //LogInfo("Initialized Dualshock");
            }
            catch (Exception e)
            {
                LogError("Could not initialize Dualshock" + e);
                isInitialized = false;
            }

            return isInitialized;
        }

        public override void Shutdown()
        {
            if (!isInitialized)
                return;

            try
            {
                if (GlobalVarRegistry.GetVariable<bool>($"{DeviceName}_disconnect_when_stop"))
                {
                    _device.DisconnectBT();
                    _device.DisconnectDongle();
                }

                _sendColor = _initColor.ToColor;
                SendColor(null, null);

                _device.StopUpdate();
                DS4Devices.stopControllers();
                isInitialized = false;
            }
            catch (Exception e)
            {
                LogError("There was an error shutting down DualShock: " + e);
                isInitialized = true;
            }
        }

        public override bool UpdateDevice(Dictionary<DeviceKeys, System.Drawing.Color> keyColors, DoWorkEventArgs e, bool forced = false)
        {
            if (keyColors.TryGetValue(key, out var clr))
            {
                _sendColor = CorrectAlpha(clr);
                return true;
            }

            return false;
        }

        protected override void RegisterVariables(VariableRegistry local)
        {
            local.Register($"{DeviceName}_devicekey", DeviceKeys.Peripheral, "Key to Use", DeviceKeys.MOUSEPADLIGHT15, DeviceKeys.Peripheral_Logo);
            local.Register($"{DeviceName}_disconnect_when_stop", false, "Disconnect when Stopping");
        }

        private void SendColor(object sender, EventArgs e)
        {
            _state.LightBarExplicitlyOff = _sendColor.R == 0 && _sendColor.G == 0 && _sendColor.B == 0;
            _state.LightBarColor = new DS4Color(_sendColor.R, _sendColor.G, _sendColor.B);
            _device.pushHapticState(_state);
        }

        private void UpdateProperties(object sender, EventArgs e)
        {
            _battery = _device.getBattery();
            _latency = _device.Latency;
            _charging = _device.isCharging();
        }
    }
}