using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using System.Threading;

namespace omori_autopatcher
{
    public class Server
    {
        private NamedPipeServerStream _server = new NamedPipeServerStream("omori-autopatcher-pipe", PipeDirection.InOut, 1, PipeTransmissionMode.Byte);
        private bool _connected;

        public Server()
        {
            new Thread(() =>
            {
                while (true)
                {
                    while (_connected)
                    {
                        
                    }
                    _server.WaitForConnection();
                    _connected = true;
                    Debug.Print("Client connected");
                }
            }).Start();
        }
        
        public bool WaitForConnection(int timeout)
        {
            if (_connected) return true;
            var wait = 0;

            while (wait < timeout && !_connected)
            {
                Thread.Sleep(100);
                wait += 100;
            }
            return _connected;
        }

        private void WriteBytes(byte[] bytes)
        {
            foreach (var b in bytes)
            {
                _server.WriteByte(b);
            }
        }

        public bool Decrypt(string targetFile, string outputPath)
        {
            // A ":" is safe here since paths can't have colons in them on windows
            WriteBytes(Encoding.UTF8.GetBytes($"{targetFile}:{outputPath}\0"));

            return _server.ReadByte() == 1;
        }
    }
}