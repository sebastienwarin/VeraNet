// -----------------------------------------------------------------------
// <copyright file="Switch.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects.Devices
{

    /// <summary>
    /// Represent a Window covering device.
    /// </summary>
    [VeraDevice(DeviceCategory.WindowCovering)]
    public class WindowCovering : DimmableLight
    {
        /// <summary>
        /// Up the Window covering.
        /// </summary>
        /// <returns></returns>
        public bool Up()
        {
            return this.SetActionAndWaitJob("urn:upnp-org:serviceId:WindowCovering1", "Up", string.Empty, null);
        }

        /// <summary>
        /// Down the Window covering.
        /// </summary>
        /// <returns></returns>
        public bool Down()
        {
            return this.SetActionAndWaitJob("urn:upnp-org:serviceId:WindowCovering1", "Down", string.Empty, null);
        }

        /// <summary>
        /// Stop the Window covering.
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            return this.SetActionAndWaitJob("urn:upnp-org:serviceId:WindowCovering1", "Stop", string.Empty, null);
        }
    }
}
