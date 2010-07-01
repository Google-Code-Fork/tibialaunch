using System;
using Keyrox.Shared;

namespace KTibiaX.IPChanger {
    public class Memory {

        public Memory(IntPtr handleProcess) {
            HandleProcess = handleProcess;
            if (HandleProcess == IntPtr.Zero)
                throw new ArgumentException("Process Handle must be defined!");

            Writer = new MemoryWriter(handleProcess);
            Reader = new MemoryReader(handleProcess);
        }

        public IntPtr HandleProcess { get; set; }

        public MemoryWriter Writer { get; set; }

        public MemoryReader Reader { get; set; }

        public class MemoryWriter {

            public MemoryWriter(IntPtr handleProcess) {
                HandleProcess = handleProcess;
            }

            public IntPtr HandleProcess { get; set; }

            public void Bytes(IntPtr memoryAddress, byte[] value) {
                IntPtr ptrBytesWritten;
                WindowsAPI.WriteProcessMemory(HandleProcess, memoryAddress, value, (uint)value.Length, out ptrBytesWritten);
            }

            public void Uint(IntPtr memoryAddress, uint value, int size) {
                uint[] nValor = { value };
                IntPtr ptrBytesWritten;
                WindowsAPI.WriteProcessMemory(HandleProcess, memoryAddress, nValor, Convert.ToUInt32(size), out ptrBytesWritten);
            }

            public void String(uint memoryAddress, string value) {
                byte[] nBuffer = new byte[value.Length];
                for (int i = 0; i < value.Length; i++) { nBuffer[i] = Convert.ToByte(value[i]); }
                Bytes((IntPtr)memoryAddress, nBuffer);
            }

            public void Double(uint memoryAddress, double value) {
                byte[] buffer = BitConverter.GetBytes(value);
                for (int i = 0; i < buffer.Length; i++) {
                    this.Bytes((IntPtr)(memoryAddress + i), new byte[] { buffer[i] });
                }
            }
        }

        public class MemoryReader {
            public MemoryReader(IntPtr handleProcess) {
                HandleProcess = handleProcess;
            }

            public IntPtr HandleProcess { get; set; }

            public string String(IntPtr memoryAddress) {
                byte[] Buffer;
                Byte((uint)memoryAddress, 100, out Buffer);

                string sBuffer = "";
                for (int i = 0; i < Buffer.Length; i++) {
                    if (Convert.ToChar(Buffer[i]).ToString() != "\0") { sBuffer += Convert.ToChar(Buffer[i]).ToString(); }
                    else { break; }
                }
                return sBuffer;
            }

            public void Byte(uint memoryAddress, uint bytesToRead, out byte[] buffer) {
                buffer = new byte[bytesToRead];
                IntPtr ptrBytesRead;
                WindowsAPI.ReadProcessMemory(HandleProcess, (IntPtr)memoryAddress, buffer, bytesToRead, out ptrBytesRead);
            }

            public void Uint(uint memoryAddress, uint bytesToRead, out uint buffer) {
                uint[] nbuffer = new uint[1];
                IntPtr ptrBytesRead;
                WindowsAPI.ReadProcessMemory(HandleProcess, (IntPtr)memoryAddress, nbuffer, bytesToRead, out ptrBytesRead);
                buffer = nbuffer[0];
            }

            public void Double(uint memoryAddress, out double value) {
                var buffer = new byte[8];
                for (int i = 0; i < buffer.Length; i++) {
                    uint result = 0x0;
                    Uint(Convert.ToUInt32(memoryAddress + i), 1, out result);
                    buffer[i] = Convert.ToByte(result);
                }
                value = BitConverter.ToDouble(buffer, 0);
            }
        }
    }
 
}
