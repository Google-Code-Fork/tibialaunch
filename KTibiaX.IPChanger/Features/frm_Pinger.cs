using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using KTibiaX.Shared.Objects;
using KTibiaX.IPChanger.Data.DTO;

namespace KTibiaX.IPChanger.Features {
    public partial class frm_Pinger : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Pinger"/> class.
        /// </summary>
        public frm_Pinger() {
            InitializeComponent();
            TMUpd.Enabled = true;
            TMPing = new System.Threading.Timer(new TimerCallback(TMPing_Callback), null, 0, 1500);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Pinger"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="port">The port.</param>
        public frm_Pinger(string server, string port)
            : this() {
            txtIp.Text = server;
            txtPort.Text = port;
        }

        /// <summary>
        /// Handles the Load event of the frm_Pinger control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Pinger_Load(object sender, EventArgs e) {
            StatusImage = Properties.Resources.remove;
            StatusText = "Not Checked!";
        }

        /// <summary>
        /// OT Server Connection State.
        /// </summary>
        private enum OTState { Unknow = 0, Online = 1, Offline = 2 }

        #region "[rgn] Form Properties  "
        private System.Threading.Timer TMPing { get; set; }
        private List<long> PingValues { get; set; }
        private string LagHeaderValue { get; set; }
        private bool MustClearControls { get; set; }

        private Image StatusImage { get; set; }
        private string StatusText { get; set; }
        private int StatusPosition { get; set; }

        private int ErrorMargin { get { return 50; } }
        OTState ConnectionState { get; set; }
        private bool PingerConnected { get; set; }
        private string LastOnlineServer { get; set; }

        private NetworkStream stream { get; set; }
        private TcpClient tcp { get; set; }
        private Server CurrentServer { get; set; }
        private Thread ThreadCheck { get; set; }
        private Thread ThreadSInfo { get; set; }
        #endregion

        #region "[rgn] Control Events   "
        private void TMUpd_Tick(object sender, EventArgs e) {
            //Safing the Thread Form Value Change.
            if (pictureEdit1.Image != StatusImage) {
                pictureEdit1.Image = StatusImage;
            }
            if (lblStatus.Text != StatusText) {
                lblStatus.Text = StatusText;
            }
            if (progressBarControl1.Position != StatusPosition) {
                if (!TMChk.Enabled) { progressBarControl1.Position = StatusPosition; }
            }
            if (!string.IsNullOrEmpty(LagHeaderValue)) {
                gcLag.Text = LagHeaderValue;
                LagHeaderValue = string.Empty;
            }
            if (MustClearControls) {
                ClearCurrentInfo();
                MustClearControls = false;
            }
            if (CurrentServer != null) {
                BindInfo(CurrentServer);
                CurrentServer = null;
            }
        }
        private void TMChk_Tick(object sender, EventArgs e) {
            if (progressBarControl1.Position < 100) {
                progressBarControl1.Position += 10;
            }
            else { progressBarControl1.Position = 100; TMChk.Stop(); }
        }
        private void btnCheck_Click(object sender, EventArgs e) {
            if (txtIp.Text.Length < 5) {
                MessageBox.Show("Invalid server address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Check();
        }
        #endregion

        #region "[rgn] Ping Lag Checker "
        public long GetPingMs() {
            if (!string.IsNullOrEmpty(LastOnlineServer)) {
                var ping = new System.Net.NetworkInformation.Ping();
                return ping.Send(LastOnlineServer, 1000, PingPacket()).RoundtripTime;
            }
            return 0;
        }
        public byte[] PingPacket() {
            var packet = new byte[1000];
            for (int i = 0; i < packet.Length; i++) {
                packet[i] = 255;
            }
            return packet;
        }
        private void TMLag_Tick(object sender, EventArgs e) {
            if (PingerConnected) {
                if (PingValues.Count > 0) {

                    gcLag.Text = string.Concat("Lag - [ ", PingValues[0], " ]");
                    if (PingValues[0] > arcScaleComponent1.MaxValue) { PingValues[0] = (arcScaleComponent1.MaxValue - 1).ToLong(); }
                    if (PingValues[0] < arcScaleComponent1.MinValue) { PingValues[0] = (arcScaleComponent1.MinValue + 1).ToLong(); }

                    if (arcScaleComponent1.Value < PingValues[0]) {
                        if ((PingValues[0] - arcScaleComponent1.Value) > 10) {
                            arcScaleComponent1.Value += 10;
                        }
                        else { arcScaleComponent1.Value++; }
                    }
                    else if (arcScaleComponent1.Value > PingValues[0]) {
                        if ((arcScaleComponent1.Value - PingValues[0]) > 10) {
                            arcScaleComponent1.Value -= 10;
                        }
                        else { arcScaleComponent1.Value--; }
                    }
                    else { PingValues.RemoveAt(0); }
                }
            }
            else { gcLag.Text = "Lag - Checking..."; arcScaleComponent1.Value = 0; }
        }
        private void TMPing_Callback(object o) {
            if (PingerConnected) {
                var lag = GetPingMs();
                if (lag > 0) { PingValues.Add(lag); }
            }
        }
        #endregion

        #region "[rgn] Get Server Info  "
        private void ThreadSInfo_Action() {
            GetServerInfo(txtIp.Text, txtPort.Text.ToInt32());
        }
        private void GetServerInfo(string server, int port) {
            try {
                MustClearControls = true;

                var packet = new byte[] { 0x06, 0x00, 0xFF, 0xFF, 0x69, 0x6E, 0x66, 0x6F };
                tcp = new TcpClient(server, port);
                stream = tcp.GetStream();

                stream.Write(packet, 0, packet.Length);
                System.Threading.Thread.Sleep(1500);

                byte[] buffer = new byte[4096];
                var len = stream.Read(buffer, 0, buffer.Length);

                var result = buffer.Redim(len);
                var xml = GetString(result);
                if (!string.IsNullOrEmpty(xml)) {
                    CurrentServer = xml.Deserialize<Server>();
                }
                StatusText = "Server On Line!";
                StatusImage = Properties.Resources.apply;
            }
            catch (SocketException) {
                StatusText = "Server Off Line!";
                StatusImage = Properties.Resources.delete;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearCurrentInfo() {
            gcStatus.Enabled = false;
            gcInfo.Enabled = false;
            txtClient.Text = string.Empty;
            txtPlayers.Text = string.Empty;
            txtMax.Text = string.Empty;
            txtPeek.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtMap.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtServer.Text = string.Empty;
            txtUrl.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtOwner.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtVersion.Text = string.Empty;
            txtUpTime.Text = string.Empty;
            txtMotd.Text = string.Empty;
            txtMonsters.Text = string.Empty;
        }
        private void BindInfo(Server server) {
            gcStatus.Enabled = true;
            gcInfo.Enabled = true;
            if (server.Players != null) {
                txtPlayers.Text = server.Players.Online;
                txtMax.Text = server.Players.Max;
                txtPeek.Text = server.Players.Peak;
            }
            if (server.Map != null) {
                txtMap.Text = server.Map.Name;
                txtAuthor.Text = server.Map.Author;
            }
            if (server.Serverinfo != null) {
                txtClient.Text = server.Serverinfo.Client;
                txtServer.Text = server.Serverinfo.Server;
                txtUrl.Text = server.Serverinfo.Url;
                txtLocation.Text = server.Serverinfo.Location;
                txtVersion.Text = server.Serverinfo.Version;
                txtUpTime.Text = GetUpTime(server.Serverinfo.Uptime.ToInt32());
            }
            if (server.Monsters != null) {
                txtMonsters.Text = server.Monsters.Total;
            }
            if (server.Owner != null) {
                txtEmail.Text = server.Owner.Email;
                txtOwner.Text = server.Owner.Name;
            }
            txtMotd.Text = server.Motd.Replace("\n", Environment.NewLine);
        }
        #endregion

        /// <summary>
        /// Checks this instance.
        /// </summary>
        public void Check() {

            TMPing.Dispose();
            PingValues = new List<long>();
            PingValues.Add(0);
            PingerConnected = false;
            LastOnlineServer = string.Empty;
            TMPing = new System.Threading.Timer(new TimerCallback(TMPing_Callback), null, 0, 1500);

            StatusImage = Properties.Resources.remove;
            StatusText = "Checking...";
            progressBarControl1.Position = 0;
            TMChk.Start();
            TMLag.Start();

            ThreadSInfo = new Thread(ThreadSInfo_Action);
            ThreadSInfo.IsBackground = true;
            ThreadSInfo.Start();

            ThreadCheck = new Thread(THCheck);
            ThreadCheck.IsBackground = true;
            ThreadCheck.Start();
        }

        /// <summary>
        /// THs the check.
        /// </summary>
        private void THCheck() {
            try {
                Thread.Sleep(500);
                var session = new KTibiaX.IPChanger.Data.OTPinger.Session(txtIp.Text.Trim(), txtPort.Text.Trim().ToInt32());
                session.Connect();

                Thread.Sleep(500);
                PingerConnected = session.IsConnected;
                LastOnlineServer = txtIp.Text.Trim();
                TMChk.Stop();
                StatusPosition = 100;
            }
            catch (SocketException) {
                SetErrorState();
                LagHeaderValue = "Lag - Unavaliable!";
            }
            catch (Exception ex) {
                SetErrorState();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sets the state of the error.
        /// </summary>
        private void SetErrorState() {
            TMChk.Stop();
            LastOnlineServer = string.Empty;
            PingerConnected = false;
            StatusPosition = 100;
        }

        /// <summary>
        /// Gets up time.
        /// </summary>
        /// <param name="uptime">The uptime.</param>
        /// <returns></returns>
        public string GetUpTime(int uptime) {
            int hours = uptime / 3600;
            int minutes = (uptime - (hours * 3600)) / 60;
            return string.Format("{0}h {1}m", hours, minutes);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public string GetString(byte[] bytes) {
            var sNewString = "";
            for (var n = 0; n < bytes.Length; n++) { sNewString += Convert.ToChar(bytes[n]).ToString(); }
            return sNewString;
        }

        /// <summary>
        /// Handles the FormClosing event of the frm_Pinger control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void frm_Pinger_FormClosing(object sender, FormClosingEventArgs e) {
            TMLag.Dispose();
        }

    }
}