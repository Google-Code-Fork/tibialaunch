using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTibiaX.IPChanger.Data;

namespace KTibiaX.IPChanger.Modules {
    public class FrameRate {

        public FrameRate(Memory memory, Address address) {
            Memory = memory;
            Address = address;
        }

        public Memory Memory { get; set; }

        public Address Address { get; set; }

        public double FPSBToX(double value) {
            return Math.Round((1110 / value) - 5, 1);
        }

        public void SetFPS(double newFPS) {
            uint FrameRateBegin = 0x0;
            Memory.Reader.Uint(Address.ptrFrameRateBegin, 4, out FrameRateBegin);
            Memory.Writer.Double(FrameRateBegin + Address.FrameRateLimitOffset, FPSBToX(newFPS));
        }

        public double GetFPS() {
            uint FrameRateBegin = 0x0;
            double CurrentFPS = 0;
            Memory.Reader.Uint(Address.ptrFrameRateBegin, 4, out FrameRateBegin);
            Memory.Reader.Double(FrameRateBegin + Address.FrameRateCurrentOffset, out CurrentFPS);
            return CurrentFPS;
        }
    }
}
