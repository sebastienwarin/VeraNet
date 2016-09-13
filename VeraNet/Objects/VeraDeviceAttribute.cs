// -----------------------------------------------------------------------
// <copyright file="VeraDeviceAttribute.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects
{
    using System;

    /// <summary>
    /// Attribute for Vera device class to set the category
    /// </summary>
    internal class VeraDeviceAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the category of a Vera device.
        /// </summary>
        /// <value>
        /// The category of a Vera device.
        /// </value>
        public DeviceCategory Category { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VeraDeviceAttribute"/> class.
        /// </summary>
        /// <param name="category">The category.</param>
        public VeraDeviceAttribute(DeviceCategory category)
        {
            this.Category = category;
        }
    }
}
