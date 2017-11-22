using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeraNet.Objects.Devices
{
    [VeraDevice(DeviceCategory.Doorlock)]
    /// <summary>
    /// Represent a DoorLock
    /// </summary>
    public class DoorLock : Device
    {
        /// <summary>
        /// Get door is locked
        /// </summary>
        public bool Locked { get; internal set; }

        /// <summary>
        /// Check if device is responding
        /// </summary>
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

        /// <summary>
        /// Lock door
        /// </summary>
        /// <returns></returns>
        public bool LockDoor()
        {
            return this.SetActionAndWaitJob("urn:micasaverde-com:serviceId:DoorLock1", "SetTarget", "newTargetValue", 1);
        }

        /// <summary>
        /// Unlock door
        /// </summary>
        /// <returns></returns>
        public bool UnLockDoor()
        {
            return this.SetActionAndWaitJob("urn:micasaverde-com:serviceId:DoorLock1", "SetTarget", "newTargetValue", 0);
        }
    }
}
