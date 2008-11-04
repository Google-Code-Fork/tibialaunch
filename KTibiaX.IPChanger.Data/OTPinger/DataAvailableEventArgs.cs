using System;

namespace KTibiaX.IPChanger.Data.OTPinger {
    /// <summary>
    /// Encapsulates arguments of the event fired when data becomes
    /// available on the telnet socket.
    /// </summary>
    public class DataAvailableEventArgs : EventArgs {
        private string data;

        /// <summary>
        /// Creates a new instance of the DataAvailableEventArgs class.
        /// </summary>
        /// <param name="output">Output from the session.</param>
        public DataAvailableEventArgs(string output) {
            data = output;
        }

        /// <summary>
        /// Gets the data from the telnet session.
        /// </summary>
        public string Data {
            get {
                return data;
            }
        }
    }
}
