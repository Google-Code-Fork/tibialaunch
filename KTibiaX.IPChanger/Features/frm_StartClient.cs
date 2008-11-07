using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using KTibiaX.IPChanger.Data;
using KTibiaX.IPChanger.Features;
using KTibiaX.IPChanger.Modules;
using KTibiaX.IPChanger.Properties;
using KTibiaX.Shared.Objects;
using Version = KTibiaX.IPChanger.Data.Version;

namespace KTibiaX.IPChanger {
    public partial class frm_StartClient : XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_StartClient"/> class.
        /// </summary>
        public frm_StartClient() {
            InitializeComponent();
            ddlGraphics.SelectedIndex = 0;
            defaultLookAndFeel1.LookAndFeel.SetSkinStyle(Settings.Default.AppSkin);
        }

        /// <summary>
        /// Handles the Load event of the frm_StartClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_StartClient_Load(object sender, EventArgs e) {
            txtPath.Text = Settings.Default.TibiaPath;
            txtPort.Text = Settings.Default.LoginPort.ToString();
            if (Settings.Default.GraphicsEngine != "") {
                foreach (var item in ddlGraphics.Properties.Items) {
                    if (item.ToString() == Settings.Default.GraphicsEngine)
                        ddlGraphics.SelectedItem = item;
                }
            }
            ckFPS.Checked = Settings.Default.ChangeFPS;
            ckMC.Checked = Settings.Default.EnableMC;
            txtPath.Focus();
        }

        #region "[rgn] Form Properties "
        public Process TibiaClient { get; set; }
        public Memory ClientMemory { get; set; }
        public Address MemoryAddress { get; set; }

        public LoginServer CurrentServer { get; set; }

        private uint LoginMax { get { return 10; } }
        private uint LoginStep { get { return 112; } }
        private uint PortStep { get { return 100; } }
        #endregion

        /// <summary>
        /// Handles the Click event of the btnStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnStart_Click(object sender, EventArgs e) {

            //Get user defined values.
            var file = new FileInfo(txtPath.Text); var port = Convert.ToInt32(txtPort.Text);
            bool isotserver = CurrentServer != null && !OfficialLoginServers().Contains(CurrentServer.Ip);

            #region "[rgn] Fix OTServer Maps   "
            var mapPath = string.Empty;
            var defaultOTMapPath = @"\OTServMaps\";
            if (isotserver && Settings.Default.DistinctMaps) {

                if (string.IsNullOrEmpty(Settings.Default.OTMapPath)) {
                    Settings.Default.OTMapPath = string.Concat(Environment.CurrentDirectory, defaultOTMapPath);
                    Settings.Default.Save();
                    if (!new DirectoryInfo(Settings.Default.OTMapPath).Exists) {
                        new DirectoryInfo(Settings.Default.OTMapPath).Create();
                    }
                }

                var mapdir = new DirectoryInfo(Settings.Default.OTMapPath);
                if (!mapdir.Exists) {
                    Settings.Default.OTMapPath = string.Concat(Environment.CurrentDirectory, defaultOTMapPath);
                    Settings.Default.Save();
                    mapdir = new DirectoryInfo(Settings.Default.OTMapPath);
                    if (!mapdir.Exists) mapdir.Create();
                    MessageBox.Show("OT Server Maps Directory not found!\nPath restored to: " + Settings.Default.OTMapPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                var servname = CurrentServer.Ip.Replace(".", "-").Trim();
                mapdir = new DirectoryInfo(Path.Combine(mapdir.FullName, servname));
                if (!mapdir.Exists) mapdir.Create(); mapPath = mapdir.FullName;
            }
            #endregion

            #region "[rgn] Start Tibia Client  "
            //Build the command line and start the client.
            string cmdLine = "";
            if (ddlGraphics.SelectedIndex > 0) cmdLine = string.Concat("engine ", ddlGraphics.Properties.Items[ddlGraphics.SelectedIndex].Value, " ");
            if (!string.IsNullOrEmpty(mapPath)) cmdLine += string.Concat("path ", mapPath.Trim());

            Process hclient = default(Process); Memory hmemory = default(Memory); Address haddress = default(Address);
            OpenClient(file.FullName, cmdLine, ckMC.Checked, ref hclient, ref haddress, ref hmemory);
            TibiaClient = hclient; ClientMemory = hmemory; MemoryAddress = haddress;
            #endregion

            #region "[rgn] Write OT RSA Key    "
            //Write RSA Key for OT Servers.
            if (isotserver && MemoryAddress != null && MemoryAddress.RSAKey > 0) {
                uint returns = 0x0;
                KTibiaX.Shared.WindowsAPI.VirtualProtectEx(TibiaClient.Handle, (IntPtr)MemoryAddress.RSAKey, (UIntPtr)((ulong)Properties.Settings.Default.RSAKey.ToString().Length), 0x40, out returns);
                ClientMemory.Writer.String(MemoryAddress.RSAKey, Properties.Settings.Default.RSAKey.ToString());
            }
            #endregion

            #region "[rgn] Write Login Servers "
            //Fix 7.6 Memory Address
            if (MemoryAddress == null) {
                var addr = new Address(Version.v760);
                var firstserver = ClientMemory.Reader.String((IntPtr)addr.LoginServer);
                if (firstserver == OldLoginServers()[0]) {
                    MemoryAddress = addr;
                }
                else {
                    MessageBox.Show("This Tibia Version is not Supported!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            System.Threading.Thread.Sleep(1000);
            //Write the new login server.
            if (!string.IsNullOrEmpty(ctrl_ServerList1.CurrentIP)) {
                var pointer = MemoryAddress.LoginServer;
                var max = MemoryAddress.Older ? 5 : LoginMax;
                var server = ctrl_ServerList1.CurrentIP.Trim();
                server += (char)0;
                for (int i = 0; i < max; i++) {
                    ClientMemory.Writer.String(pointer, server);
                    ClientMemory.Writer.Uint((IntPtr)(pointer + PortStep), port.ToUInt32(), 2);
                    pointer += LoginStep;
                }
            }
            #endregion

            #region "[rgn] Change Frame Rate   "
            //Change FPS.
            if (ckFPS.Checked && MemoryAddress != null && MemoryAddress.ptrFrameRateBegin > 0) {
                var FPSChanger = new FrameRate(ClientMemory, MemoryAddress);
                //var currentFPS = FPSChanger.GetFPS();
                FPSChanger.SetFPS(Convert.ToDouble(Settings.Default.FPSValue));
            }
            #endregion

            #region "[rgn] Client/Version Save "
            //Save current client and version.
            var clients = Settings.Default.ClientList; if (clients == null) clients = new ClientPathCollection();
            var innerclients = clients.Where(c => c.Version == MemoryAddress.Version);
            if (innerclients.Count() > 0) { innerclients.ToList().ForEach(c => clients.Remove(c)); }
            clients.Add(new ClientPath() { Path = txtPath.Text, Version = MemoryAddress.Version });
            Settings.Default.ClientList = clients; Settings.Default.Save();
            #endregion

            //Good bye.
            Close();
        }

        /// <summary>
        /// Opens the client.
        /// </summary>
        /// <param name="clientPath">The client path.</param>
        /// <param name="commandLine">The command line.</param>
        /// <param name="mc">if set to <c>true</c> [mc].</param>
        private static void OpenClient(string clientPath, string commandLine, bool mc, ref Process process, ref Address address, ref Memory memory) {

            #region "[rgn] Load Security Info  "
            //Get Client Path Info.
            var file = new FileInfo(clientPath);
            if (!file.Exists) throw new FileNotFoundException("Tibia Client not found!");

            //Get User Token
            IntPtr token = IntPtr.Zero;
            var sa = new ProcessTools.Security_attributes();
            ProcessTools.GetUserToken(ref sa, ref token);
            #endregion

            #region "[rgn] Start Tibia Client  "
            //Execute Client.
            ProcessTools.StartupInfo si = new ProcessTools.StartupInfo();
            ProcessTools.Process_Information pi = new ProcessTools.Process_Information();
            var success = ProcessTools.CreateProcessAsUser(token, file.FullName, commandLine, ref sa, ref sa, false, 4, IntPtr.Zero, file.Directory.FullName, ref si, out pi);
            System.Threading.Thread.Sleep(1000);
            //Get the Process
            if (!success) { MessageBox.Show("Failed to start Tibia.exe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            var TibiaClient = System.Diagnostics.Process.GetProcessById(pi.dwProcessId.ToInt32());

            //Get Client Information.
            var hProcess = ProcessTools.OpenProcess(TibiaClient);
            var ClientMemory = new Memory(hProcess);
            Address MemoryAddress = null;
            //ProcessTools.CloseHandle(TibiaClient.Handle);
            #endregion

            #region "[rgn] Fix Tibia Version   "
            //Verify the Client Version.
            for (int i = 0; i < Versions.Total; i++) {
                var addr = new Address((Version)i);
                string nversion = ClientMemory.Reader.String((IntPtr)addr.RSAKey);
                if (nversion.StartsWith(Properties.Settings.Default.StringVersion)) {
                    MemoryAddress = addr;
                    //Fix the bug between 830 and 831
                    if (MemoryAddress.Version == Version.v830) {
                        string mcResult = ClientMemory.Reader.String((IntPtr)MemoryAddress.MultiClient);

                        if (mcResult != Properties.Settings.Default.MCVersion) {
                            MemoryAddress = new Address(Version.v831);
                        }
                    }
                    //MessageBox.Show(MemoryAddress.Version.ToString());
                    break;
                }
            }
            #endregion

            #region "[rgn] Enable Multi-Client "
            //Enable Multi-Client
            if (mc) {
                ClientMemory.Writer.Bytes((IntPtr)MemoryAddress.MultiClient, new byte[] { 0x90 });
                ClientMemory.Writer.Bytes((IntPtr)(MemoryAddress.MultiClient + 1), new byte[] { 0x90 });
            }
            #endregion

            #region "[rgn] Relase Tibia Thread "
            //Free and show the thread.
            ProcessTools.ResumeThread(pi.hThread);
            System.Threading.Thread.Sleep(1000);
            #endregion

            //Fill reference values.
            process = TibiaClient;
            memory = ClientMemory;
            address = MemoryAddress;
        }


        #region "[rgn] Helper Methods  "
        public List<string> OfficialLoginServers() {
            var result = new List<string>();
            result.Add("login01.tibia.com");
            result.Add("login02.tibia.com");
            result.Add("login03.tibia.com");
            result.Add("login04.tibia.com");
            result.Add("login05.tibia.com");
            result.Add("tibia01.cipsoft.com");
            result.Add("tibia02.cipsoft.com");
            result.Add("tibia03.cipsoft.com");
            result.Add("tibia04.cipsoft.com");
            result.Add("tibia05.cipsoft.com");
            return result;
        }
        public List<string> OldLoginServers() {
            var result = new List<string>();
            result.Add("server.tibia.com");
            result.Add("server2.tibia.com");
            result.Add("tibia1.cipsoft.com");
            result.Add("tibia2.cipsoft.com");
            return result;
        }
        #endregion

        #region "[rgn] Control Events  "
        private void txtPath_ButtonClick(object sender, ButtonPressedEventArgs e) {
            openFileDialog1.ShowDialog();
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {
            txtPath.Text = openFileDialog1.FileName;
        }
        private void ctrl_ServerList1_ServerChange(object sender, LoginServerChangeEventArgs e) {
            if (e.LoginServer != null) {
                CurrentServer = e.LoginServer;
                txtPort.Text = e.LoginServer.Port.ToString();

                if (Settings.Default.ClientList != null) {
                    var clients = (from client in Properties.Settings.Default.ClientList where client.Version == e.LoginServer.Version select client);
                    if (clients.Count() > 0) {
                        txtPath.Text = clients.First().Path;
                    }
                }
            }
        }
        private void btnAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var frmAbout = new frm_About();
            this.Hide();
            frmAbout.FormClosed += new FormClosedEventHandler(frmAbout_FormClosed);
            frmAbout.Show();
        }
        private void frmAbout_FormClosed(object sender, FormClosedEventArgs e) {
            this.Show();
        }
        private void btnChecker_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            frm_Pinger frmChecker;
            frmChecker = CurrentServer != null ? new frm_Pinger(CurrentServer.Ip, CurrentServer.Port.ToString()) : new frm_Pinger();
            this.Hide();
            frmChecker.FormClosed += new FormClosedEventHandler(frmChecker_FormClosed);
            frmChecker.Show();

        }
        private void frmChecker_FormClosed(object sender, FormClosedEventArgs e) {
            this.Show();
        }
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Application.ExitThread();
        }
        private void btnSkin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var frmSkin = new frm_Skins();
            frmSkin.FormClosed += new FormClosedEventHandler(frmSkin_FormClosed);
            this.Hide();
            frmSkin.Show();
        }
        private void frmSkin_FormClosed(object sender, FormClosedEventArgs e) {
            this.Show();
            this.defaultLookAndFeel1.LookAndFeel.SetSkinStyle(Settings.Default.AppSkin);
            this.Refresh(); this.Update();
        }
        private void btnOptions_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var frmOptions = new frm_Options();
            frmOptions.FormClosed += frmOptions_FormClosed;
            frmOptions.OptionsChanged += frmOptions_OptionsChanged;
            this.Hide();
            frmOptions.Show();
        }
        private void frmOptions_OptionsChanged(object sender, IPCOptionsEventArgs e) {
            ckFPS.Checked = e.Args.ChangeFPS;
            ckMC.Checked = e.Args.EnableMC;
            if (Settings.Default.GraphicsEngine != "") {
                foreach (ImageComboBoxItem item in ddlGraphics.Properties.Items) {
                    if (item.ToString() == Settings.Default.GraphicsEngine)
                        ddlGraphics.SelectedIndex = ddlGraphics.Properties.Items.IndexOf(item);
                }
            }
        }
        private void frmOptions_FormClosed(object sender, FormClosedEventArgs e) {
            this.Show();
        }
        #endregion

        /// <summary>
        /// Handles the FormClosing event of the frm_StartClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void frm_StartClient_FormClosing(object sender, FormClosingEventArgs e) {
            Settings.Default.TibiaPath = txtPath.Text;
            Settings.Default.LoginServer = CurrentServer != null ? CurrentServer.Ip : "";
            Settings.Default.GraphicsEngine = ddlGraphics.SelectedItem.ToString();
            Settings.Default.LoginPort = Convert.ToInt32(txtPort.Text);
            Settings.Default.Save();
        }
    }
}