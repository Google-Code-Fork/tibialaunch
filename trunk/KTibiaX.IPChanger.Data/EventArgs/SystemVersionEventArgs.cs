using System;
using KTibiaX.IPChanger.Data.Objects;

namespace KTibiaX.IPChanger.Data {
    public class SystemVersionEventArgs : EventArgs {

        public SystemVersionEventArgs(SystemVersion args) {
            SystemVersion = args;
        }

        public SystemVersion SystemVersion { get; set; }

    }
}
