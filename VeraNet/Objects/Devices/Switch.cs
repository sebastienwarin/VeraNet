// -----------------------------------------------------------------------
// <copyright file="Switch.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects.Devices
{
    using System.Collections.Generic;

    /// <summary>
    /// Represent a Switch device.
    /// </summary>
    [VeraDevice(DeviceCategory.Switch)]
    public class Switch : PowerMeter
    {
        /// <summary>
        /// Gets the statis of this <see cref="Switch"/>.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the switch is ON; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; internal set; }
        
        internal override void InitializeProperties(Dictionary<string, object> values)
        {
            base.InitializeProperties(values);
            this.Status = (bool)(values["status"].ToString() == "1");
        }

        internal override void UpdateProperties(Dictionary<string, object> values)
        {
            base.UpdateProperties(values);
            this.UpdateProperty(values, "status", "Status", (v) => { this.Status = (bool)(v.ToString() == "1"); return true; });
        }

        /// <summary>
        /// Switch ON.
        /// </summary>
        /// <returns></returns>
        public bool SwitchOn()
        {
            return this.SetActionAndWaitJob("urn:upnp-org:serviceId:SwitchPower1", "SetTarget", "newTargetValue", 1);
        }

        /// <summary>
        /// Switch OFF.
        /// </summary>
        /// <returns></returns>
        public bool SwitchOff()
        {
            return this.SetActionAndWaitJob("urn:upnp-org:serviceId:SwitchPower1", "SetTarget", "newTargetValue", 0);
        }
    }
}
