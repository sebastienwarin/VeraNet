// -----------------------------------------------------------------------
// <copyright file="TemperatureSensor.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects.Devices
{
    using System.Collections.Generic;

    /// <summary>
    /// Represent a temperature sensors.
    /// </summary>
    [VeraDevice(DeviceCategory.TemperatureSensor)]
    public class TemperatureSensor : Device
    {
        /// <summary>
        /// Gets the current temperature.
        /// </summary>
        /// <value>
        /// The temperature.
        /// </value>
        public double Temperature { get; internal set; }

        internal override void InitializeProperties(Dictionary<string, object> values)
        {
            base.InitializeProperties(values);
            this.Temperature = double.NaN;
            if (values.ContainsKey("temperature"))
                this.Temperature = double.Parse(values["temperature"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
        }

        internal override void UpdateProperties(Dictionary<string, object> values)
        {
            base.UpdateProperties(values);
            this.UpdateProperty(values, "temperature", "Temperature", (v) => { this.Temperature = double.Parse(v.ToString(), System.Globalization.CultureInfo.InvariantCulture); return true; });
        }
    }
}
