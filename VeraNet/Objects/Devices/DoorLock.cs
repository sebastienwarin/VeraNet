using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeraNet.Objects.Devices
{
    [VeraDevice(DeviceCategory.Doorlock)]
    public class DoorLock : Device
    {
        public bool Locked { get; internal set; }
        public bool CommFailure { get; internal set; }

        internal override void InitializeProperties(Dictionary<string, object> values)
        {
            base.InitializeProperties(values);
            this.CommFailure = (bool)(values["commFailure"].ToString() == "1");
            this.Locked = (bool)(values["locked"].ToString() == "1");
        }

        internal override void UpdateProperties(Dictionary<string, object> values)
        {
            base.UpdateProperties(values);
            this.UpdateProperty(values, "commFailure", "CommFailure", (v) => { this.CommFailure = (bool)(v.ToString() == "1"); return true; });
            this.UpdateProperty(values, "locked", "Locked", (v) => { this.Locked = (bool)(v.ToString() == "1"); return true; });
        }

        public bool LockDoor()
        {
            return this.SetActionAndWaitJob("urn:micasaverde-com:serviceId:DoorLock1", "SetTarget", "newTargetValue", 1);
        }

        public bool UnLockDoor()
        {
            return this.SetActionAndWaitJob("urn:micasaverde-com:serviceId:DoorLock1", "SetTarget", "newTargetValue", 0);
        }
    }
}
