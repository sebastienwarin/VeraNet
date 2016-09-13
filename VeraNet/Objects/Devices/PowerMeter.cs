// -----------------------------------------------------------------------
// <copyright file="PowerMeter.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects.Devices
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represent a power meter device
    /// </summary>
    [VeraDevice(DeviceCategory.PowerMeter)]
    public class PowerMeter : Device
    {
        /// <summary>
        /// Gets the current watts.
        /// </summary>
        /// <value>
        /// The current watts.
        /// </value>
        public double Watts { get; internal set; }
        /// <summary>
        /// Gets the consumption in KWh
        /// </summary>
        /// <value>
        /// The consumption in KWh
        /// </value>
        public decimal KWh { get; internal set; }

        internal override void InitializeProperties(Dictionary<string, object> values)
        {
            base.InitializeProperties(values);
            this.Watts = values.ContainsKey("watts") && !string.IsNullOrEmpty(values["watts"].ToString()) ? double.Parse(values["watts"].ToString(), System.Globalization.CultureInfo.InvariantCulture) : Int32.MinValue;
            this.KWh = values.ContainsKey("kwh") && !string.IsNullOrEmpty(values["kwh"].ToString()) ? decimal.Parse(values["kwh"].ToString(), System.Globalization.CultureInfo.InvariantCulture) : Int32.MinValue;
        }

        internal override void UpdateProperties(Dictionary<string, object> values)
        {
            base.UpdateProperties(values);
            this.UpdateProperty(values, "watts", "Watts", (v) =>
            {
                if (string.IsNullOrEmpty(v.ToString())) return false;
                this.Watts = double.Parse(v.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                return true;
            });
            this.UpdateProperty(values, "kwh", "KWh", (v) =>
            {
                if (string.IsNullOrEmpty(v.ToString())) return false;
                this.KWh = decimal.Parse(v.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                return true;
            });
        }
    }
}
