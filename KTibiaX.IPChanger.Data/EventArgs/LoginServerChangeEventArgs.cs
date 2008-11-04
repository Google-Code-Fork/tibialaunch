using System;

namespace KTibiaX.IPChanger.Data {
    public class LoginServerChangeEventArgs : EventArgs {

        public LoginServerChangeEventArgs(LoginServer args) {
            LoginServer = args;
        }

        public LoginServer LoginServer { get; set; }
    }
}
