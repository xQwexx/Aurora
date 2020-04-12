using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Aurora;
using Aurora.Devices;
using Aurora.Devices.SteelSeries;
using Aurora.Settings;
using Aurora.Utils;
using Newtonsoft.Json.Linq;

namespace Device_SteelSeries
{
    public partial class SteelSeriesDevice : Device
    {
        protected override string DeviceName => "SteelSeries";

        object lock_obj = new object();

        public override bool Initialize()
        {
            lock (lock_obj)
            {
                try
                {
                    if (!baseObject.ContainsKey("game"))
                    {
                        baseObject.Add("game", "PROJECTAURORA");
                        baseColorObject.AddFirst(new JProperty("game", baseObject["game"]));
                    }
                    client = new HttpClient {Timeout = TimeSpan.FromSeconds(30)};
                    loadCoreProps();
                    return true;
                }
                catch (Exception e)
                {
                    LogError(e, "SteelSeries SDK could not be initialized.");
                    return false;
                }
            }
        }

        public override void Shutdown()
        {
            lock (lock_obj)
            {
                pingTaskTokenSource.Cancel();
                client?.Dispose();
                isInitialized = false;
            }
        }

        public override void Reset()
        {
            Shutdown();
            lock (lock_obj)
            {
                Process.Start(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\SteelSeries\SteelSeries Engine 3\SteelSeries Engine 3.lnk");
                Task.Delay(15000).GetAwaiter().GetResult();
            }
            Initialize();
        }

        public override bool UpdateDevice(Dictionary<DeviceKeys, Color> keyColors, DoWorkEventArgs e, bool forced = false)
        {
            dataColorObject.RemoveAll();
            if (!Global.Configuration.devices_disable_mouse || !Global.Configuration.devices_disable_headset)
            {
                if (!keyColors.ContainsKey(DeviceKeys.Peripheral))
                {
                    var mousePad = keyColors.Where(t => t.Key >= DeviceKeys.MOUSEPADLIGHT1 && t.Key <= DeviceKeys.MOUSEPADLIGHT12).Select(t => t.Value).ToArray();
                    var mouse = new List<Color> { keyColors[DeviceKeys.Peripheral_Logo], keyColors[DeviceKeys.Peripheral_ScrollWheel]};
                    //mouse.AddRange(keyColors.Where(t => t.Key <= DeviceKeys.MOUSELIGHT1 && t.Key >= DeviceKeys.MOUSELIGHT6).Select(t => t.Value));
                    setOneZone(keyColors[DeviceKeys.Peripheral_Logo]);
                    if (mouse.Count <= 1)
                        setMouse(keyColors[DeviceKeys.Peripheral_Logo]);
                    else
                    {
                        setLogo(keyColors[DeviceKeys.Peripheral_Logo]);
                        setWheel(keyColors[DeviceKeys.Peripheral_ScrollWheel]);
                        if (mouse.Count == 8)
                            setEightZone(mouse.ToArray());
                    }
                    if (mousePad.Length == 2)
                        setTwoZone(mousePad);
                    else
                        setTwelveZone(mousePad);
                    //if (keyColors.Count(t => t.Key >= DeviceKeys.MONITORLIGHT1 && t.Key <= DeviceKeys.MONITORLIGHT103) == 103)
                    //    setHundredThreeZone(keyColors.Where(t => t.Key >= DeviceKeys.MONITORLIGHT1 && t.Key <= DeviceKeys.MONITORLIGHT103).Select(t => t.Value).ToArray());
                }
                else
                    setGeneric(keyColors[DeviceKeys.Peripheral]);
            }
            if (!Global.Configuration.devices_disable_keyboard)
            {
                foreach (var (key, color) in keyColors)
                {
                    if (TryGetHid(key, out var hid))
                    {
                        setKeyboardLed(hid, color);
                    }
                }
            }
            sendLighting();
            return true;
        }
    }
}