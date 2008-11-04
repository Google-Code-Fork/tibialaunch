using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTibiaX.IPChanger.Data.OTPinger {
    /// <summary>
    /// A simple telnet session.
    /// </summary>
    public class Session {
        public TelnetWrapper Telnet{ get; set; }
        private bool done = false;

        /// <summary>
        /// Gets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected { get { return Telnet.Connected; } }
                
        /// <summary>
        /// Creates a new instance of the Session class with the specified host
        /// and port.
        /// </summary>
        /// <param name="host">Remote address/hostname</param>
        /// <param name="port">Remote port</param>
        public Session(string host, int port) {
            Telnet = new TelnetWrapper();

            Telnet.Disconnected += new DisconnectedEventHandler(this.OnDisconnect);
            Telnet.DataAvailable += new DataAvailableEventHandler(this.OnDataAvailable);

            Telnet.TerminalType = "NETWORK-VIRTUAL-TERMINAL";
            Telnet.Hostname = host;
            Telnet.Port = port;
        }

        /// <summary>
        /// Connects to the remote host.
        /// </summary>
        /// <returns>Success of connection.</returns>
        public bool Connect() {
            Telnet.Connect();
            return Telnet.Connected;
        }

        /// <summary>
        /// Gets the completion status of the current session.
        /// </summary>
        public bool IsDone {
            get {
                return done;
            }
        }

        /// <summary>
        /// Starts a new scripted session.
        /// </summary>
        public void ScriptedSession() {
            Console.WriteLine(Environment.NewLine + "Scripted sessions not implemented!");
            Telnet.Disconnect();
            Console.ReadLine();
        }

        /// <summary>
        /// Starts a new interactive session.
        /// </summary>
        public void InteractiveSession() {
            try {
                Telnet.Receive();

                while (!done)
                    Telnet.Send(Console.ReadLine() + Telnet.CR);
            }
            catch {
                Telnet.Disconnect();
                throw;
            }
        }

        private void OnDisconnect(object sender, EventArgs e) {
            done = true;
            Console.WriteLine(Environment.NewLine + "Disconnected.");
            Console.WriteLine("Press [Enter] to quit.");
        }

        private void OnDataAvailable(object sender, DataAvailableEventArgs e) {
            Console.Write(e.Data);
        }
    }
}
