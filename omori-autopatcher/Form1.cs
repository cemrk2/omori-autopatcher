using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace omori_autopatcher
{
    public partial class Form1 : Form
    {
        private Server _server = new Server();

        public Form1()
        {
            InitializeComponent();
            statusLbl.ForeColor = Color.White;
            this.patchBtn.ForeColor = Color.White;
            this.dumpBtn.ForeColor = Color.White;
            this.dumpExeBtn.ForeColor = Color.White;
            this.BackColor = Color.FromArgb(255, 31, 31, 31);
            this.progressBar.Hide();
        }

        private void CopyFiles(string outputDir, string chowdrenPath)
        {
            var files = Utils.GetFiles(chowdrenPath).ToArray();
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var name = file.Substring(chowdrenPath.Length + 1);
                var parentDir = Directory.GetParent(outputDir + "/" + name)?.FullName;
                if (file.EndsWith("Chowdren.exe")) continue;

                if (!Directory.Exists(parentDir)) Directory.CreateDirectory(parentDir);
                File.Copy(file, outputDir + "/" + name, true);
                statusLbl.Invoke(
                    new MethodInvoker(delegate
                    {
                        statusLbl.Text = $"Copying ({i}/{files.Length - 1}) {name}";
                    }));
                var percentage = (Math.Floor((float)i / files.Length*100f) - 15);
                if (percentage > 1)
                {
                    progressBar.Invoke(new MethodInvoker(delegate { progressBar.Value = (int)(16 + percentage); }));
                }
            }
        }

        private void DumpExecutable(string exePath)
        {
            MessageBox.Show(exePath);
            progressBar.Invoke(new MethodInvoker(delegate
            {
                progressBar.Value = 0;
                progressBar.Show(); 
            }));
            statusLbl.Invoke(new MethodInvoker(delegate
            {
                statusLbl.ForeColor = Color.White;
                statusLbl.Text = @"Attaching DLL to Chowdren process";
            }));

            Process chowdrenProcess;
            try
            {
                Utils.LoadDLL(out chowdrenProcess);
            }
            catch (Exception ex)
            {
                statusLbl.Invoke(new MethodInvoker(delegate
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = ex.Message;
                }));
                
                return;
            }
            
            progressBar.Invoke(new MethodInvoker(delegate { progressBar.Value = 33; }));
            statusLbl.Invoke(new MethodInvoker(delegate { statusLbl.Text = @"Waiting for connection..."; }));

            if (!_server.WaitForConnection(10000))
            {
                statusLbl.Invoke(new MethodInvoker(delegate
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = @"Failed to attach. Client timed out.";
                }));
                
                return;
            }
            
            progressBar.Invoke(new MethodInvoker(delegate { progressBar.Value = 66; }));
            statusLbl.Invoke(new MethodInvoker(delegate { statusLbl.Text = @"Decrypting Chowdren.exe..."; }));

            if (!_server.Decrypt("Chowdren.exe", exePath))
            {
                statusLbl.Invoke(new MethodInvoker(delegate
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = @"Failed to decrypt Chowdren.exe";
                }));
                
                return;
            }

            try
            {
                chowdrenProcess.Kill();
            }
            catch (Exception ignored)
            {
                // ignored
            }
            
            progressBar.Invoke(new MethodInvoker(delegate { progressBar.Value = 100; }));
            statusLbl.Invoke(new MethodInvoker(delegate
            {
                statusLbl.ForeColor = Color.Lime;
                statusLbl.Text = @"Game dumped successfully!";
            }));
        }

        private void DumpGame(string outputDir)
        {
            if (outputDir.EndsWith("\\")) outputDir = outputDir.Substring(0, outputDir.Length - 1);

            progressBar.Invoke(new MethodInvoker(delegate
            {
                progressBar.Value = 0;
                progressBar.Show(); 
            }));
            statusLbl.Invoke(new MethodInvoker(delegate
            {
                statusLbl.ForeColor = Color.White;
                statusLbl.Text = @"Attaching DLL to Chowdren process";
            }));
            Process chowdrenProcess;
            try
            {
                Utils.LoadDLL(out chowdrenProcess);
            }
            catch (Exception ex)
            {
                statusLbl.Invoke(new MethodInvoker(delegate
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = ex.Message;
                }));
                
                return;
            }

            if (chowdrenProcess.MainModule == null)
            {
                statusLbl.Invoke(new MethodInvoker(delegate
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = @"Failed to find main module of Chowdren process";
                }));
                
                return;
            }
            var chowdrenPath = Directory.GetParent(chowdrenProcess.MainModule.FileName).FullName;
            Debug.Print("chowdrenPath: {0}", chowdrenPath);

            if (!Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);

            progressBar.Invoke(new MethodInvoker(delegate { progressBar.Value = 5; }));
            statusLbl.Invoke(new MethodInvoker(delegate { statusLbl.Text = @"Waiting for connection..."; }));

            if (!_server.WaitForConnection(10000))
            {
                statusLbl.Invoke(new MethodInvoker(delegate
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = @"Failed to attach. Client timed out.";
                }));
                
                return;
            }
            
            progressBar.Invoke(new MethodInvoker(delegate { progressBar.Value = 10; }));
            statusLbl.Invoke(new MethodInvoker(delegate { statusLbl.Text = @"Decrypting Chowdren.exe"; }));

            if (!_server.Decrypt("Chowdren.exe", Path.GetFullPath(outputDir + "\\Chowdren.exe")))
            {
                statusLbl.Invoke(new MethodInvoker(delegate
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = @"Failed to decrypt Chowdren.exe";
                }));
                
                return;
            }
            
            statusLbl.Invoke(new MethodInvoker(delegate { statusLbl.Text = @"Unloading DLL"; }));
            if (!_server.Decrypt("", "freeLibrary"))
            {
                statusLbl.Invoke(new MethodInvoker(delegate
                {
                    statusLbl.ForeColor = Color.Red;
                    statusLbl.Text = @"Failed to unload DLL";
                }));
                
                return;
            }
            statusLbl.Invoke(new MethodInvoker(delegate { statusLbl.Text = @"DLL Unloaded"; }));
            progressBar.Invoke(new MethodInvoker(delegate { progressBar.Value = 15; }));
            CopyFiles(outputDir, chowdrenPath);
            try
            {
                chowdrenProcess.Kill();
            }
            catch (Exception ignored)
            {
                // ignored
            }
            
            statusLbl.Invoke(new MethodInvoker(delegate
            {
                statusLbl.ForeColor = Color.Lime;
                statusLbl.Text = @"Game dumped successfully!";
            }));

            MessageBox.Show(@"Game successfully dumped!", @"OMORI AutoPatcher", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void dumpBtn_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();
            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(fbd.SelectedPath)) return;

            new Thread(() =>
            {
                DumpGame(fbd.SelectedPath);
            }).Start();
        }

        private void dumpExeBtn_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Portable Executable Files | *.exe";
            var result = sfd.ShowDialog();
            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(sfd.FileName)) return;

            new Thread(() =>
            {
                DumpExecutable(sfd.FileName);
            }).Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Exit not so gracefully
            Environment.Exit(0);
        }
    }
}