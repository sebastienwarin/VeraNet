// -----------------------------------------------------------------------
// <copyright file="VeraConnectionInfo.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet
{
    /// <summary>
    /// Represent a connection informations to Vera
    /// </summary>
    public class VeraConnectionInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether this is a local or remote connection.
        /// </summary>
        /// <value>
        /// <c>true</c> if this is a local connection; otherwise, <c>false</c>.
        /// </value>
        public bool IsLocalConnection { get; set; }

        /// <summary>
        /// Gets or sets the local ip.
        /// </summary>
        /// <value>
        /// The local ip.
        /// </value>
        public string LocalIP { get; set; }
        /// <summary>
        /// Gets or sets the local port.
        /// </summary>
        /// <value>
        /// The local port.
        /// </value>
        public int LocalPort { get; set; }

        /// <summary>
        /// Gets or sets the remote user.
        /// </summary>
        /// <value>
        /// The remote user.
        /// </value>
        public string RemoteUser { get; set; }
        /// <summary>
        /// Gets or sets the remote password.
        /// </summary>
        /// <value>
        /// The remote password.
        /// </value>
        public string RemotePassword { get; set; }
        /// <summary>
        /// Gets or sets the remote serial.
        /// </summary>
        /// <value>
        /// The remote serial.
        /// </value>
        public int RemoteSerial { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VeraConnectionInfo"/> class for local access.
        /// </summary>
        /// <param name="localIp">The local ip.</param>
        /// <param name="localPort">The local port (3480 by default).</param>
        public VeraConnectionInfo(string localIp, int localPort = 3480)
        {
            this.IsLocalConnection = true;
            this.LocalIP = localIp;
            this.LocalPort = localPort;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VeraConnectionInfo"/> class for remote access.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="sn">The sn.</param>
        public VeraConnectionInfo(string user, string password, int sn)
        {
            this.IsLocalConnection = false;
            this.RemotePassword = password;
            this.RemoteSerial = sn;
            this.RemoteUser = user;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.IsLocalConnection ? string.Format("http://{0}:{1}", this.LocalIP, this.LocalPort) :
                                            string.Format("https://fwd2.mios.com/{0}/{1}/{2}", this.RemoteUser, this.RemotePassword, this.RemoteSerial);
        }
    }
}
