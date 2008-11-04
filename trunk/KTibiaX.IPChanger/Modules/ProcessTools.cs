using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace KTibiaX.IPChanger {
    public class ProcessTools {

        [StructLayout(LayoutKind.Sequential)]
        public struct StartupInfo {
            public int cb;
            public String lpReserved;
            public String lpDesktop;
            public String lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Process_Information {
            public IntPtr hProcess;
            public IntPtr hThread;
            public uint dwProcessId;
            public uint dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Security_attributes {
            public int Length;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        [DllImport("kernel32.dll", EntryPoint = "CloseHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public extern static bool CloseHandle(IntPtr handle);

        [DllImport("advapi32.dll", EntryPoint = "CreateProcessAsUser", SetLastError = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static bool CreateProcessAsUser(IntPtr hToken, String lpApplicationName, String lpCommandLine, ref Security_attributes lpProcessAttributes,
            ref Security_attributes lpThreadAttributes, bool bInheritHandle, int dwCreationFlags, IntPtr lpEnvironment,
            String lpCurrentDirectory, ref StartupInfo lpStartupInfo, out Process_Information lpProcessInformation);

        [DllImport("advapi32.dll", EntryPoint = "DuplicateTokenEx")]
        public extern static bool DuplicateTokenEx(IntPtr ExistingTokenHandle, uint dwDesiredAccess,
            ref Security_attributes lpThreadAttributes, int TokenType,
            int ImpersonationLevel, ref IntPtr DuplicateTokenHandle);

        [DllImport("kernel32.dll")]
        public static extern bool CreateProcess(
            string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes,
            bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment,
            string lpCurrentDirectory, ref StartupInfo lpStartupInfo, out Process_Information lpProcessInformation);

        [DllImport("kernel32.dll")]
        public static extern uint ResumeThread(IntPtr hThread);

        public static IntPtr OpenProcess(Process wprocess) {
            var access = KTibiaX.Shared.WindowsAPI.ProcessAccessRights.PROCESS_VM_READ |
                KTibiaX.Shared.WindowsAPI.ProcessAccessRights.PROCESS_VM_WRITE |
                KTibiaX.Shared.WindowsAPI.ProcessAccessRights.PROCESS_VM_OPERATION;

            var hprocess = KTibiaX.Shared.WindowsAPI.OpenProcess((uint)access, 0, (uint)wprocess.Id);
            if (hprocess.ToInt32() == 0) { throw new Exception("OpenProcess Failed!"); }
            return hprocess;
        }

        /// <summary>
        /// Gets the user token.
        /// </summary>
        /// <param name="userSA">Security Attributes.</param>
        /// <param name="userToken">User token.</param>
        public static void GetUserToken(ref Security_attributes userSA, ref IntPtr userToken) {
            IntPtr Token = new IntPtr(0);
            IntPtr DupedToken = new IntPtr(0);
            bool ret;

            var sa = new Security_attributes();
            sa.bInheritHandle = false;
            sa.Length = Marshal.SizeOf(sa);
            sa.lpSecurityDescriptor = (IntPtr)0;
            Token = WindowsIdentity.GetCurrent().Token;

            const uint GENERIC_ALL = 0x10000000;
            const int SecurityImpersonation = 2;
            const int TokenType = 1;

            ret = DuplicateTokenEx(Token, GENERIC_ALL, ref sa, SecurityImpersonation, TokenType, ref DupedToken);
            if (ret == false) throw new Exception("DuplicateTokenEx failed with " + Marshal.GetLastWin32Error());
            userSA = sa;
            userToken = DupedToken;
        }
    }
}
