// -----------------------------------------------------------------------
// <copyright file="VeraEventArgs.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet
{
    using System;
    using VeraNet.Objects;

    /// <summary>
    ///  Provides data when a device is updated
    /// </summary>
    public class DeviceUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the device updated.
        /// </summary>
        /// <value>
        /// The device updated.
        /// </value>
        public Device Device { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceUpdatedEventArgs"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public DeviceUpdatedEventArgs(Device device)
        {
            this.Device = device;
        }
    }

    /// <summary>
    ///  Provides data when a scene is updated
    /// </summary>
    public class SceneUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the scene updated.
        /// </summary>
        /// <value>
        /// The scene updated.
        /// </value>
        public Scene Scene { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneUpdatedEventArgs"/> class.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public SceneUpdatedEventArgs(Scene scene)
        {
            this.Scene = scene;
        }
    }

    /// <summary>
    /// Provides data when the house mode changes
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class HouseModeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the new mode.
        /// </summary>
        /// <value>
        /// The new mode.
        /// </value>
        public VeraHouseMode NewMode { get; set; }
        /// <summary>
        /// Gets or sets the old mode.
        /// </summary>
        /// <value>
        /// The old mode.
        /// </value>
        public VeraHouseMode OldMode { get; set; }
    }

        /// <summary>
        ///  Provides data when an error occcured.
        /// </summary>
        public class VeraErrorOccurredEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VeraErrorOccurredEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public VeraErrorOccurredEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }

    /// <summary>
    ///  Provides data when data is received from the Vera device.
    /// </summary>
    public class VeraDataReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the data version.
        /// </summary>
        /// <value>
        /// The data version.
        /// </value>
        public long DataVersion { get; set; }
        /// <summary>
        /// Gets or sets the load time.
        /// </summary>
        /// <value>
        /// The load time.
        /// </value>
        public long LoadTime { get; set; }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public long Length { get; set; }
        /// <summary>
        /// Gets or sets the raw data.
        /// </summary>
        /// <value>
        /// The raw data.
        /// </value>
        public string RawData { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VeraDataReceivedEventArgs"/> class.
        /// </summary>
        public VeraDataReceivedEventArgs()
        {
            this.Date = DateTime.Now;
        }
    }

    /// <summary>
    ///  Provides data when data is sent to the Vera device
    /// </summary>
    public class VeraDataSentEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the length of the data sent.
        /// </summary>
        /// <value>
        /// The length of the data sent.
        /// </value>
        public long Length { get; set; }
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public string Uri { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VeraDataSentEventArgs"/> class.
        /// </summary>
        public VeraDataSentEventArgs()
        {
            this.Date = DateTime.Now;
        }
    }
}
