using System;

namespace KTibiaX.IPChanger.Data {
    public class TibiaCFGEventArgs : EventArgs {

        public TibiaCFGEventArgs(TibiaCFG args) {
            TibiaCFG = args;
        }

        public TibiaCFG TibiaCFG { get; set; }

    }
}
