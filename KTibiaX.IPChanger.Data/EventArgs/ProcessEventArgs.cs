using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace KTibiaX.IPChanger.Data {
    public class ProcessEventArgs : EventArgs {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessEventArgs"/> class.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <param name="server">The server.</param>
        public ProcessEventArgs(Process process, LoginServer server) {
            ClientProcess = process;
            Server = server;
        }

        /// <summary>
        /// Gets or sets the client process.
        /// </summary>
        /// <value>The client process.</value>
        public Process ClientProcess { get; private set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public LoginServer Server { get; private set; }

    }
}
