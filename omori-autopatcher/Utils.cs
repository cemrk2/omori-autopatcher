using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace omori_autopatcher
{
    internal class Utils
    {
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        
        [Flags]
        public enum AllocationType : uint
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection : uint
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }
        
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        [DllImport("kernel32.dll", SetLastError=true, ExactSpelling=true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess,IntPtr lpAddress,
            uint dwSize, uint flAllocationType, uint flProtect);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(
            ProcessAccessFlags processAccess,
            bool bInheritHandle,
            uint processId
        );
        
        private static IntPtr OpenProcess(Process proc, ProcessAccessFlags flags)
        {
            return OpenProcess(flags, false, (uint)proc.Id);
        }
        
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            Int32 nSize,
            out IntPtr lpNumberOfBytesWritten);
        
        [DllImport("kernel32.dll", SetLastError=true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);
        
        [DllImport("kernel32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
        public static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);
        
        [DllImport("kernel32", CharSet=CharSet.Ansi, ExactSpelling=true, SetLastError=true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess,
            IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress,
            IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);
        
        internal static void LoadDLL(out Process oProcess)
        {
            var processes = Process.GetProcesses();
            var processFound = false;
            Process target = null;
            foreach (var process in processes)
            {
                var dllPath = Path.GetFullPath("libomori-autopatcher.dll");
#if DEBUG
                if (File.Exists(@"../../../../x64/Debug/libomori-autopatcher.dll"))
                    dllPath = Path.GetFullPath(@"../../../../x64/Debug/libomori-autopatcher.dll");
#endif
                if (process.ProcessName != "Chowdren") continue;
                processFound = true;
                target = process;
                
                var handle = OpenProcess(process, ProcessAccessFlags.All);
                if (handle == INVALID_HANDLE_VALUE)
                {
                    throw new Exception("Attaching failed. Failed to open handle to Chowdren.exe");
                }
                
                Debug.Print("handle: {0:D}", handle);

                var address = VirtualAllocEx(handle, IntPtr.Zero, (uint)dllPath.Length + 1, (uint)AllocationType.Commit,
                    (uint)MemoryProtection.ReadWrite);

                if (address == IntPtr.Zero)
                {
                    throw new Exception("Attaching failed. Failed to allocate buffer inside Chowdren process");
                }
                
                Debug.Print("VirtualAllocEx: 0x{0:X}", address);

                var termPath = new List<byte>(Encoding.ASCII.GetBytes(dllPath)) { 0 };
                WriteProcessMemory(handle, address, termPath.ToArray(), termPath.Count,
                    out var bytesWritten);
                
                Debug.Print("WriteProcessMemory: {0:D}", bytesWritten.ToInt64());

                var kernel32 = GetModuleHandle("kernel32.dll");
                if (kernel32 == INVALID_HANDLE_VALUE)
                {
                    throw new Exception("Attaching failed. Failed to get handle to kernel32.dll");
                }
                
                Debug.Print("kernel32.dll handle: 0x{0:X}", kernel32.ToInt64());

                var loadLibraryA = GetProcAddress(kernel32, "LoadLibraryA");
                if (loadLibraryA == IntPtr.Zero)
                {
                    throw new Exception("Attaching failed. Failed to get address of LoadLibraryA");
                }
                
                Debug.Print("LoadLibraryA: 0x{0:X}", loadLibraryA.ToInt64());

                var threadHandle = CreateRemoteThread(handle, IntPtr.Zero, 0, loadLibraryA, address, 0, out var threadId);
                if (threadHandle == INVALID_HANDLE_VALUE)
                {
                    throw new Exception("Attaching failed. Failed to spawn remote thread inside Chowdren process");
                }
                
                Debug.Print("CreateRemoteThread: handle={0:D}, threadId={1:D}", threadHandle.ToInt64(), threadId.ToInt64());

                CloseHandle(handle);
            }

            if (!processFound)
            {
                throw new Exception("Failed to find Chowdren process");
            }
            oProcess = target;
        }
        
        public static IEnumerable<string> GetFiles(string path) {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0) {
                path = queue.Dequeue();
                try {
                    foreach (string subDir in Directory.GetDirectories(path)) {
                        queue.Enqueue(subDir);
                    }
                }
                catch(Exception ex) {
                    Console.Error.WriteLine(ex);
                }
                string[] files = null;
                try {
                    files = Directory.GetFiles(path);
                }
                catch (Exception ex) {
                    Console.Error.WriteLine(ex);
                }
                if (files != null) {
                    for(int i = 0 ; i < files.Length ; i++) {
                        yield return files[i];
                    }
                }
            }
        }
    }
}