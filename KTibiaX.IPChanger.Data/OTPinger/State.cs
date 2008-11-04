using System;
using System.Net.Sockets;

namespace KTibiaX.IPChanger.Data.OTPinger {
    /// <summary>
    /// State object for receiving data from remote device.
    /// </summary>
    public class State {
        /// <summary>
        /// Size of receive buffer.
        /// </summary>
        public const int BufferSize = 256;
        /// <summary>
        /// Client socket.
        /// </summary>
        public Socket WorkSocket = null;
        /// <summary>
        /// Receive buffer.
        /// </summary>
        public byte[] Buffer = new byte[BufferSize];
    }
}
