// -----------------------------------------------------------------------
// <copyright file="HumiditySensor.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects.Devices
{
    using System.Collections.Generic;

    /// <summary>
    /// Represent an humidity sensor
    /// </summary>
    [VeraDevice(DeviceCategory.HimiditySensor)]
    public class HumiditySensor : Device
    {
        /// <summary>
        /// Gets the current humidity.
        /// </summary>
        /// <value>
        /// The current humidity.
        /// </value>
        public double Humidity { get; internal set; }

        internal override void InitializeProperties(Dictionary<string, object> values)
        {
            base.InitializeProperties(values);
            this.Humidity = double.Parse(values["humidity"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
        }

        internal override void UpdateProperties(Dictionary<string, object> values)
        {
            base.UpdateProperties(values);
            this.UpdateProperty(values, "humidity", "Humidity", (v) => { this.Humidity = double.Parse(v.ToString(), System.Globalization.CultureInfo.InvariantCulture); return true; });
        }
    }
}
