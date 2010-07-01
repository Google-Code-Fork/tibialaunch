using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace KTibiaX.IPChanger.Data.OTPinger {
    /// <summary>
    /// TelnetWrapper is a sample class demonstrating the use of the 
    /// telnet protocol handler.
    /// </summary>
    public class TelnetWrapper : TelnetProtocolHandler {

        #region Globals and properties


        // ManualResetEvent instances signal completion.
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);

        public event DisconnectedEventHandler Disconnected;
        public event DataAvailableEventHandler DataAvailable;

        private Socket socket;

        protected string hostname;
        protected int port;

        /// <summary>
        /// Sets the name of the host to connect to.
        /// </summary>
        public string Hostname {
            set {
                hostname = value;
            }
        }

        /// <summary>
        /// Sets the port on the remote host.
        /// </summary>
        public int Port {
            set {
                if (value > 0)
                    port = value;
                else
                    throw (new ArgumentException("Port number must be greater than 0.", "Port"));
            }
        }

        /// <summary>
        /// Gets the TTL.
        /// </summary>
        /// <value>The TTL.</value>
        public int TTL { get { return (int)socket.Ttl; } }

        /// <summary>
        /// Sets the terminal width.
        /// </summary>
        public int TerminalWidth {
            set {
                windowSize.Width = value;
            }
        }

        /// <summary>
        /// Sets the terminal height.
        /// </summary>
        public int TerminalHeight {
            set {
                windowSize.Height = value;
            }
        }

        /// <summary>
        /// Sets the terminal type.
        /// </summary>
        public string TerminalType {
            set {
                terminalType = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a connection to the remote
        /// resource exists.
        /// </summary>
        public bool Connected {
            get {
                return socket.Connected;
            }
        }

        #endregion

        #region Public interface

        /// <summary>
        /// Connects to the remote host  and opens the connection.
        /// </summary>
        public void Connect() {
            Connect(hostname, port);
        }

        /// <summary>
        /// Connects to the specified remote host on the specified port
        /// and opens the connection.
        /// </summary>
        /// <param name="host">Hostname of the Telnet server.</param>
        /// <param name="port">The Telnet port on the remote host.</param>
        public void Connect(string host, int port) {
            try {
                var ipHostInfo = Dns.GetHostEntry(host);
                var ipAddress = ipHostInfo.AddressList[0];
                var remoteEP = new IPEndPoint(ipAddress, port);

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), socket);
                connectDone.WaitOne();

                Reset();
            }
            catch (Exception) { Disconnect(); throw; }
        }

        /// <summary>
        /// Sends a command to the remote host.
        /// </summary>
        /// <param name="cmd">the command</param>
        /// <returns>output of the command</returns>
        public string Send(string cmd) {
            try {
                var arr = Encoding.ASCII.GetBytes(cmd);
                Transpose(arr);
                return null;
            }
            catch (Exception e) {
                Disconnect();
                throw (new ApplicationException("Error writing to socket.", e));
            }
        }

        /// <summary>
        /// Starts receiving data.
        /// </summary>
        public void Receive() {
            Receive(socket);
        }

        /// <summary>
        /// Disconnects the socket and closes the connection.
        /// </summary>
        public void Disconnect() {
            if (socket != null && socket.Connected) {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                Disconnected(this, new System.EventArgs());
            }
        }

        #endregion

        #region IO methods
        /// <summary>
        /// Writes data to the socket.
        /// </summary>
        /// <param name="b">the buffer to be written</param>
        protected override void Write(byte[] b) {
            if (socket.Connected)
                Send(socket, b);
            sendDone.WaitOne();
        }

        /// <summary>
        /// Callback for the connect operation.
        /// </summary>
        /// <param name="ar">Stores state information for this asynchronous 
        /// operation as well as any user-defined data.</param>
        private void ConnectCallback(IAsyncResult ar) {
            Socket client = null;

            try {
                client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                connectDone.Set();
            }
            catch (Exception/* e*/) {
                Disconnect();
                connectDone.Set();
            }
        }

        /// <summary>
        /// Begins receiving for the data coming from the socket.
        /// </summary>
        /// <param name="client">The socket to get data from.</param>
        private void Receive(Socket client) {
            try {
                State state = new State();
                state.WorkSocket = client;
                client.BeginReceive(state.Buffer, 0, State.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e) {
                Disconnect();
                throw (new ApplicationException("Error on read from socket.", e));
            }
        }

        /// <summary>
        /// Callback for the receive operation.
        /// </summary>
        /// <param name="ar">Stores state information for this asynchronous 
        /// operation as well as any user-defined data.</param>
        private void ReceiveCallback(IAsyncResult ar) {
            try {
                State state = (State)ar.AsyncState;
                Socket client = state.WorkSocket;

                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0) {
                    InputFeed(state.Buffer, bytesRead);
                    Negotiate(state.Buffer);
                    DataAvailable(this, new DataAvailableEventArgs(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead)));
                    client.BeginReceive(state.Buffer, 0, State.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
                else { Disconnect(); }
            }
            catch (Exception/* e*/) {
                Disconnect();
                //throw (new ApplicationException("Error reading from socket.", e));
            }
        }

        /// <summary>
        /// Writes data to the socket.
        /// </summary>
        /// <param name="client">The socket to write to.</param>
        /// <param name="byteData">The data to write.</param>
        private void Send(Socket client, byte[] byteData) {
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        /// <summary>
        /// Callback for the send operation.
        /// </summary>
        /// <param name="ar">Stores state information for this asynchronous 
        /// operation as well as any user-defined data.</param>
        private void SendCallback(IAsyncResult ar) {
            Socket client = (Socket)ar.AsyncState;
            int bytesSent = client.EndSend(ar);
            sendDone.Set();
        }

        #endregion

        protected override void SetLocalEcho(bool echo) { }

        protected override void NotifyEndOfRecord() { }

        #region Cleanup

        public void Close() {
            Dispose();
        }

        public void Dispose() {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected void Dispose(bool disposing) {
            if (disposing)
                Disconnect();
        }

        ~TelnetWrapper() {
            Dispose(false);
        }

        #endregion
    }

    #region Event handlers

    /// <summary>
    /// A delegate type for hooking up disconnect notifications.
    /// </summary>
    public delegate void DisconnectedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// A delegate type for hooking up data available notifications.
    /// </summary>
    public delegate void DataAvailableEventHandler(object sender, DataAvailableEventArgs e);

    #endregion
}
