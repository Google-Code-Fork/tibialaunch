using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace KTibiaX.IPChanger.Data {
    public class ProcessEventArgs : EventArgs {

        public ProcessEventArgs(Process process, LoginServer server) {
            Process = process;
            Server = server;
        }

        public Process Process { get; private set; }

        public LoginServer Server { get; private set; }

    }
}
