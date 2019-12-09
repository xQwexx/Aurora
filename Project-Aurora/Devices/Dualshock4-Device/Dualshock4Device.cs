using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Devices;
using DS4Windows;

namespace Dualshock4_Device
{
    public class Dualshock4Device : Device
    {
        protected override string DeviceName => "Dualshock 4";
        public int Battery { get; private set; }
        public double Latency { get; private set; }
        public bool Charging { get; private set; }

        private DS4HapticState _state;
        private DS4Color _initColor;
        private DS4Device _device;
        private DeviceKeys key;
        private Color _sendColor;

        new public string GetDeviceDetails()
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

                details += Charging ? " ⚡" : " ";
                details += "🔋" + Battery + "% Delay: " + Latency.ToString("0.00") + " ms";

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

            key = variableRegistry.GetVariable<DeviceKeys>($"{DeviceName}_devicekey");
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
                if (variableRegistry.GetVariable<bool>($"{DeviceName}_disconnect_when_stop"))
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
            _sendColor = CorrectAlpha(keyColors[key]);
            return true;
        }

        protected override void RegisterVariables()
        {
            variableRegistry.Register($"{DeviceName}_devicekey", DeviceKeys.Peripheral, "Key to Use", DeviceKeys.MOUSEPADLIGHT15, DeviceKeys.Peripheral_Logo);
            variableRegistry.Register($"{DeviceName}_disconnect_when_stop", false, "Disconnect when Stopping");
        }

        private void SendColor(object sender, EventArgs e)
        {
            _state.LightBarExplicitlyOff = _sendColor.Equals(Color.Black);
            _state.LightBarColor = new DS4Color(_sendColor.R, _sendColor.G, _sendColor.B);
            _device.pushHapticState(_state);
        }

        private void UpdateProperties(object sender, EventArgs e)
        {
            Battery = _device.getBattery();
            Latency = _device.Latency;
            Charging = _device.isCharging();
        }
    }
}
