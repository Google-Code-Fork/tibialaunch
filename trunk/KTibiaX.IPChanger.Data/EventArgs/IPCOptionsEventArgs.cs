using System;

namespace KTibiaX.IPChanger.Data {
    public class IPCOptionsEventArgs : EventArgs {

        public IPCOptionsEventArgs(IPCOptions args) {
            Args = args;
        }

        public IPCOptions Args { get; set; }
    }
}
