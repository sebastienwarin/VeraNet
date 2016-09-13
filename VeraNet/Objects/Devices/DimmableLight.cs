// -----------------------------------------------------------------------
// <copyright file="DimmableLight.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects.Devices
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represent a dimmable light
    /// </summary>
    [VeraDevice(DeviceCategory.DimmableLight)]
    public class DimmableLight : Switch
    {
        /// <summary>
        /// Gets the level of this dimmable light.
        /// </summary>
        /// <value>
        /// The leve of this dimmable light.
        /// </value>
        public int Level { get; internal set; }

        internal override void InitializeProperties(Dictionary<string, object> values)
        {
            base.InitializeProperties(values);
            this.Level = Convert.ToInt32(values["level"]);
        }

        internal override void UpdateProperties(Dictionary<string, object> values)
        {
            base.UpdateProperties(values);
            this.UpdateProperty(values, "level", "Level", (v) => { this.Level = Convert.ToInt32(v); return true; });
        }

        /// <summary>
        /// Sets the level of this dimmable light.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        public bool SetLevel(int level)
        { 
            if(level >= 0 && level <= 100)
            {
                return this.SetActionAndWaitJob("urn:upnp-org:serviceId:Dimming1", "SetLoadLevelTarget", "newLoadlevelTarget", level);
            }
            return false;
        }
    }
}
