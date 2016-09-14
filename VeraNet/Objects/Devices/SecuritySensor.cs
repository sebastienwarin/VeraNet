// -----------------------------------------------------------------------
// <copyright file="SecuritySensor.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects.Devices
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represent a Security sensor
    /// </summary>
    [VeraDevice(DeviceCategory.SecuritySensor)]
    public class SecuritySensor : Device
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="SecuritySensor"/> is armed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if armed; otherwise, <c>false</c>.
        /// </value>
        public bool Armed { get; internal set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="SecuritySensor"/> is tripped when armed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if is tripped when armed; otherwise, <c>false</c>.
        /// </value>
        public bool ArmedTripped { get; internal set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="SecuritySensor"/> is tripped.
        /// </summary>
        /// <value>
        ///   <c>true</c> if tripped; otherwise, <c>false</c>.
        /// </value>
        public bool Tripped { get; internal set; }
        /// <summary>
        /// Gets the DateTime of the last trip.
        /// </summary>
        /// <value>
        /// The DateTime of the last trip.
        /// </value>
        public DateTime LastTrip { get; internal set; }

        internal override void InitializeProperties(Dictionary<string, object> values)
        {
            base.InitializeProperties(values);
            this.Armed = (bool)(values["armed"].ToString() == "1");
            this.ArmedTripped = (bool)(values["armedtripped"].ToString() == "1");
            this.Tripped = (bool)((values.ContainsKey("tripped") ? values["tripped"] : values["armedtripped"]).ToString() == "1");
            this.LastTrip = values.ContainsKey("lasttrip") ? this.GetDateTime(Convert.ToInt64(values["lasttrip"])) : DateTime.Now;
        }

        internal override void UpdateProperties(Dictionary<string, object> values)
        {
            base.UpdateProperties(values);
            this.UpdateProperty(values, "armed", "Armed", (v) => { this.Armed = (bool)(v.ToString() == "1"); return true; });
            this.UpdateProperty(values, "armedtripped", "ArmedTripped", (v) => { this.ArmedTripped = (bool)(v.ToString() == "1"); return true; });
            this.UpdateProperty(values, "tripped", "Tripped", (v) => { this.Tripped = (bool)(v.ToString() == "1"); return true; });
            this.UpdateProperty(values, "lasttrip", "LastTrip", (v) => { this.LastTrip = values.ContainsKey("lasttrip") ? this.GetDateTime(Convert.ToInt64(values["lasttrip"])) : DateTime.Now; return true; });
        }
    }
}
