using Aurora.Devices;
using Colore;
using Colore.Effects.ChromaLink;
using Colore.Effects.Headset;
using Colore.Effects.Keyboard;
using Colore.Effects.Keypad;
using Colore.Effects.Mouse;
using Colore.Effects.Mousepad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Device_Razer
{
    public class RazerDevice : Device
    {
        protected override string DeviceName => "Razer";

        private IChroma chroma;

        private KeyboardCustom keyboard = KeyboardCustom.Create();
        private MousepadCustom mousepad = MousepadCustom.Create();
        private MouseCustom mouse = MouseCustom.Create();
        private HeadsetCustom headset = HeadsetCustom.Create();
        private KeypadCustom keypad = KeypadCustom.Create();
        private ChromaLinkCustom chromalink = ChromaLinkCustom.Create();

        private List<string> deviceNames;

        public override bool Initialize()
        {
            try
            {
                //hack
                chroma = ColoreProvider.CreateNativeAsync().Result;

                DetectDevices();

                isInitialized = true;
                return true;
            }
            catch (Exception e)
            {
                LogError(e.Message);
                isInitialized = false;
                return false;
            }
        }

        public override string GetDeviceDetails()
        {
            if (isInitialized)
            {
                string devString = DeviceName + ": ";
                devString += "Connected";

                if (deviceNames.Any())
                    devString += ": " + string.Join(", ", deviceNames);
                else
                    devString += ": no devices detected";

                return devString;
            }
            else
            {
                return DeviceName + ": Not initialized";
            }
        }

        public override void Shutdown()
        {
            try
            {
                chroma.SetAllAsync(new Colore.Data.Color(0, 0, 0));
                chroma.UninitializeAsync();
                isInitialized = false;
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
        }

        public override bool UpdateDevice(Dictionary<DeviceKeys, System.Drawing.Color> keyColors, DoWorkEventArgs e, bool forced = false)
        {
            foreach (var key in keyColors)
            {
                if (RazerMappings.keyboardDictionary.TryGetValue(key.Key, out var kbIndex))
                    keyboard[kbIndex] = ToColore(key.Value);

                if (RazerMappings.mouseDictionary.TryGetValue(key.Key, out var mouseIndex))
                    mouse[mouseIndex] = ToColore(key.Value);

                if (RazerMappings.mousepadDictionary.TryGetValue(key.Key, out var mousepadIndex))
                    mousepad[mousepadIndex] = ToColore(key.Value);

                if (RazerMappings.headsetDictionary.TryGetValue(key.Key, out var headsetIndex))
                    headset[headsetIndex] = ToColore(key.Value);

                if (RazerMappings.chromalinkDictionary.TryGetValue(key.Key, out var chromalinkIndex))
                    chromalink[chromalinkIndex] = ToColore(key.Value);
            }
            UpdateAll();
            return true;
        }

        private static Colore.Data.Color ToColore(System.Drawing.Color source) => new Colore.Data.Color(source.R, source.G, source.B);

        private void DetectDevices()
        {
            //get all devices from colore, with the respective names and Guids
            IEnumerable<(string Name, Guid Guid)> DeviceGuids = typeof(Colore.Data.Devices).GetFields().Select(f =>
                    ((Attribute.GetCustomAttribute(f, typeof(DescriptionAttribute), false) as DescriptionAttribute).Description,
                    (Guid)f.GetValue(null)));

            deviceNames = new List<string>();

            foreach (var device in DeviceGuids.Where(d => d.Name != "Razer Core Chroma"))//somehow this device is unsupported, can't query it
            {
                try
                {
                    var devInfo = chroma.QueryAsync(device.Guid).Result;
                    if (devInfo.Connected)
                    {
                        deviceNames.Add(device.Name);
                    }
                }
                catch (AggregateException e)
                {
                    if ((e.InnerException is Colore.Native.NativeCallException) && (e.InnerException as Colore.Native.NativeCallException).Result.Value == 1167)
                    {
                        //Global.logger.Info("device NOT connected: " + device.Name);
                    }
                    else
                    {
                        //Global.logger.Info(e);
                    }
                }
            }
        }

        private void UpdateAll()
        {
            chroma.Keyboard.SetCustomAsync(keyboard);
            chroma.Mouse.SetGridAsync(mouse);
            chroma.Headset.SetCustomAsync(headset);
            chroma.Mousepad.SetCustomAsync(mousepad);
            chroma.Keypad.SetCustomAsync(keypad);
            chroma.ChromaLink.SetCustomAsync(chromalink);
        }
    }
}
