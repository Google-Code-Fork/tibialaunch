using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using KTibiaX.IPChanger.Data;
using KTibiaX.IPChanger.Features;
using KTibiaX.IPChanger.Modules;
using KTibiaX.IPChanger.Properties;
using KTibiaX.Shared.Objects;
using Version = KTibiaX.IPChanger.Data.Version;
using KTibiaX.IPChanger.Data.Objects;

namespace KTibiaX.IPChanger {
    public partial class frm_StartClient : XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_StartClient"/> class.
        /// </summary>
        public frm_StartClient() {
            InitializeComponent();
            ddlGraphics.SelectedIndex = 0;
            TMUpdateValues.Start();
            defaultLookAndFeel1.LookAndFeel.SetSkinStyle(Settings.Default.AppSkin);

            CheckServerList();
            CheckConfigFileList();
            CheckClientVersionList();
            lblUpdate.Caption = Program.GetCurrentResource().GetString("strCheckingUpdate");
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
            UpdateChecker = new CheckUpdate();
            UpdateChecker.NewVersionDetected += new EventHandler<SystemVersionEventArgs>(UpdateChecker_NewVersionDetected);
            UpdateChecker.NoUpdateAvailable += new EventHandler(UpdateChecker_NoUpdateAvailable);
            UpdateChecker.WebNotAvailable += new EventHandler(UpdateChecker_WebNotAvailable);
            UpdateChecker.BeginCheckVersion();

            lblVersion.Caption = string.Concat("v", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            txtPath.Focus();
        }

        #region "[rgn] Form Properties "
        public Process TibiaClient { get; set; }
        public Memory ClientMemory { get; set; }
        public Address MemoryAddress { get; set; }

        public LoginServer CurrentServer { get; set; }
        public CheckUpdate UpdateChecker { get; set; }
        public string UpdateLabelText { get; set; }
        public bool MustHideMainForm { get; set; }
        public SystemVersion NewVersion { get; set; }

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
            var oficialConfigFile = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\\Tibia\\tibia.cfg");


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
                    MessageBox.Show(Program.GetCurrentResource().GetString("strMapError") + Settings.Default.OTMapPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                var servname = CurrentServer.Ip.Replace(".", "-").Trim();
                mapdir = new DirectoryInfo(Path.Combine(mapdir.FullName, servname));
                if (!mapdir.Exists) mapdir.Create(); mapPath = mapdir.FullName;
            }
            #endregion

            #region "[rgn] Fix Config File     "
            if (ctrl_ConfigFile1.CurrentConfigFile != null) {

                if (ctrl_ServerList1.CurrentServer != null) {
                    if (ctrl_ConfigFile1.CurrentConfigFile.Version != ctrl_ServerList1.CurrentServer.Version) {
                        MessageBox.Show(Program.GetCurrentResource().GetString("strVersionConflict"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                FileInfo configDefaultPath = null;
                if (isotserver && Settings.Default.DistinctMaps) { configDefaultPath = new FileInfo(string.Concat(mapPath, "\\tibia.cfg")); }
                else { configDefaultPath = new FileInfo(oficialConfigFile); }

                if (configDefaultPath.Exists) configDefaultPath.Delete();
                var newconfigFile = new FileInfo(ctrl_ConfigFile1.CurrentConfigFile.Path);
                if (newconfigFile.Exists) { newconfigFile.CopyTo(configDefaultPath.FullName); }
                else {
                    MessageBox.Show(Program.GetCurrentResource().GetString("strConfigFileNotFound"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    var files = Settings.Default.ConfigFiles;
                    files.Remove(ctrl_ConfigFile1.CurrentConfigFile);
                    Settings.Default.ConfigFiles = files;
                    Settings.Default.Save();
                    ctrl_ConfigFile1.CurrentConfigFile = null;
                }
            }
            #endregion

            #region "[rgn] Start Tibia Client  "
            //Build the command line and start the client.
            string cmdLine = "";
            if (ddlGraphics.SelectedIndex > 0) cmdLine = string.Concat(" engine ", ddlGraphics.Properties.Items[ddlGraphics.SelectedIndex].Value, " ");
            if (!string.IsNullOrEmpty(mapPath)) cmdLine += string.Concat("path ", mapPath.Trim());

            Process hclient = default(Process); Memory hmemory = default(Memory); Address haddress = default(Address);
            OpenClient(file.FullName, cmdLine, Settings.Default.EnableMC, ref hclient, ref haddress, ref hmemory);
            TibiaClient = hclient; ClientMemory = hmemory; MemoryAddress = haddress;
            if (TibiaClient == null) { return; }
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
                    MessageBox.Show(Program.GetCurrentResource().GetString("strVersionNotSupported"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (Settings.Default.ChangeFPS && MemoryAddress != null && MemoryAddress.ptrFrameRateBegin > 0) {
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

            //Close program if necessary.
            if (Settings.Default.CloseAfterStart) Close();
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
            if (!file.Exists) {
                MessageBox.Show(Program.GetCurrentResource().GetString("strClientNotFound"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
            if (!success) { MessageBox.Show(Program.GetCurrentResource().GetString("strStartFail"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
        private void btnKeyrox_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var url = Program.GetCurrentResource().GetString("strUrl");
            var info = new ProcessStartInfo("iexplore.exe");
            info.Arguments = url;
            Process.Start(info);
        }
        private void btnLanguage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var frmCulture = new frm_Culture();
            frmCulture.FormClosed += new FormClosedEventHandler(frmCulture_FormClosed);
            this.Hide();
            frmCulture.Show();
        }
        private void frmCulture_FormClosed(object sender, FormClosedEventArgs e) {
            MessageBox.Show(Program.GetCurrentResource().GetString("strMustRestart"), "KTibiaX", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Show();
        }
        private void lblVersion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var url = Program.GetCurrentResource().GetString("strUrl");
            var info = new ProcessStartInfo("iexplore.exe");
            info.Arguments = url;
            Process.Start(info);
        }
        private void btnUpdates_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            lblUpdate.Caption = Program.GetCurrentResource().GetString("strCheckingUpdate");
            UpdateChecker.BeginCheckVersion();
        }
        private void UpdateChecker_WebNotAvailable(object sender, EventArgs e) {
            UpdateLabelText = Program.GetCurrentResource().GetString("strUpdateError");
        }
        private void UpdateChecker_NoUpdateAvailable(object sender, EventArgs e) {
            UpdateLabelText = Program.GetCurrentResource().GetString("strNoUpdates");
        }
        private void UpdateChecker_NewVersionDetected(object sender, SystemVersionEventArgs e) {
            UpdateLabelText = Program.GetCurrentResource().GetString("strUpdateFound");
            NewVersion = e.SystemVersion;
            MustHideMainForm = true;
        }
        private void frmInfo_FormClosed(object sender, FormClosedEventArgs e) {
            this.Show();
        }
        private void TMUpdateValues_Tick(object sender, EventArgs e) {
            if (MustHideMainForm) {
                var frmInfo = new frm_UpdateInfo(NewVersion);
                frmInfo.FormClosed += new FormClosedEventHandler(frmInfo_FormClosed);
                this.Hide();
                frmInfo.Show();
                MustHideMainForm = false;
            }
            if (lblUpdate.Caption != UpdateLabelText) { lblUpdate.Caption = UpdateLabelText; }
        }
        #endregion

        #region "[rgn] Settings Backup "
        public void CheckServerList() {
            try {
                if (Settings.Default.ServerList == null) {
                    var file = new FileInfo(string.Concat(Application.StartupPath, "\\",Settings.Default.AppConfigDir, "\\ServerList.kbx"));
                    if (file.Exists) {
                        var reader = new StreamReader(file.FullName);

                        var xml = reader.ReadToEnd();
                        reader.Close(); reader.Dispose();
                        var oldServerList = xml.Deserialize<LoginServerCollection>();

                        Settings.Default.ServerList = oldServerList;
                        Settings.Default.Save();
                    }
                }
            }
            catch (Exception ex) { ex.ToString(); }
        }
        public void SaveServerList() {
            if (Settings.Default.ServerList != null) {
                var file = new FileInfo(string.Concat(Application.StartupPath, "\\", Settings.Default.AppConfigDir, "\\ServerList.kbx"));
                if (file.Exists) { file.Delete(); }
                if (!file.Directory.Exists) { file.Directory.Create(); }
                var writer = new StreamWriter(file.FullName);
                writer.Write(Settings.Default.ServerList.Serialize());
                writer.Flush(); writer.Close();
            }
        }
        public void CheckConfigFileList() {
            try {
                if (Settings.Default.ConfigFiles == null) {
                    var file = new FileInfo(string.Concat(Application.StartupPath, "\\", Settings.Default.AppConfigDir, "\\ConfigFiles.kbx"));
                    if (file.Exists) {
                        var reader = new StreamReader(file.FullName);
                        
                        var xml = reader.ReadToEnd();
                        reader.Close(); reader.Dispose();
                        var oldSettings = xml.Deserialize<TibiaCFGCollection>();

                        Settings.Default.ConfigFiles = oldSettings;
                        Settings.Default.Save();
                    }
                }
            }
            catch (Exception ex) { ex.ToString(); }
        }
        public void SaveConfigFileList() {
            if (Settings.Default.ConfigFiles != null) {
                var file = new FileInfo(string.Concat(Application.StartupPath, "\\", Settings.Default.AppConfigDir, "\\ConfigFiles.kbx"));
                if (file.Exists) { file.Delete(); }
                if (!file.Directory.Exists) { file.Directory.Create(); }
                var writer = new StreamWriter(file.FullName);
                writer.Write(Settings.Default.ConfigFiles.Serialize());
                writer.Flush(); writer.Close();
            }
        }
        public void CheckClientVersionList() {
            try {
                if (Settings.Default.ClientList == null) {
                    var file = new FileInfo(string.Concat(Application.StartupPath, "\\", Settings.Default.AppConfigDir, "\\ClientList.kbx"));
                    if (file.Exists) {
                        var reader = new StreamReader(file.FullName);

                        var xml = reader.ReadToEnd();
                        reader.Close(); reader.Dispose();
                        var oldSettings = xml.Deserialize<ClientPathCollection>();

                        Settings.Default.ClientList = oldSettings;
                        Settings.Default.Save();
                    }
                }
            }
            catch (Exception ex) { ex.ToString(); }
        }
        public void SaveClientVersionList() {
            if (Settings.Default.ClientList != null) {
                var file = new FileInfo(string.Concat(Application.StartupPath, "\\", Settings.Default.AppConfigDir, "\\ClientList.kbx"));
                if (file.Exists) { file.Delete(); }
                if (!file.Directory.Exists) { file.Directory.Create(); }
                var writer = new StreamWriter(file.FullName);
                writer.Write(Settings.Default.ClientList.Serialize());
                writer.Flush(); writer.Close();
            }
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
            
            SaveServerList();
            SaveConfigFileList();
            SaveClientVersionList();
        }

    }
}