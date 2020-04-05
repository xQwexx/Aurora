using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Devices;
using System.Drawing;

namespace Device_Steelseries
{
    public class SteelseriesDevice : Device
    {
        protected override string DeviceName => "Steelseries";

        public override bool Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Shutdown()
        {
            throw new NotImplementedException();
        }

        public override bool UpdateDevice(Dictionary<DeviceKeys, System.Drawing.Color> keyColors, DoWorkEventArgs e, bool forced = false)
        {
            throw new NotImplementedException();
        }
    }
}
