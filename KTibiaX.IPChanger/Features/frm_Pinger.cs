using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using KTibiaX.Shared.Objects;
using Timer = System.Windows.Forms.Timer;
using System.Net.Sockets;

namespace KTibiaX.IPChanger.Features {
    public partial class frm_Pinger : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Pinger"/> class.
        /// </summary>
        public frm_Pinger() {
            InitializeComponent();
            TMLag = new Timer();
            TMLag.Tick += new EventHandler(TMLag_Tick);
            TMLag.Interval = 100;
            TMLag.Enabled = false;

            TMChk = new Timer();
            TMChk.Tick += new EventHandler(TMChk_Tick);
            TMChk.Interval = 500;
            TMChk.Enabled = false;

            StatusImage = pictureEdit1.Image;
            StatusText = lblStatus.Text;

            TMUpd = new Timer();
            TMUpd.Tick += new EventHandler(TMUpd_Tick);
            TMUpd.Interval = 500;
            TMUpd.Enabled = true;

            TMPing = new System.Threading.Timer(new TimerCallback(TMPing_Callback), null, 0, 1500);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Pinger"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="port">The port.</param>
        public frm_Pinger(string server, string port) : this() {
            txtIp.Text = server;
            txtPort.Text = port;
        }

        /// <summary>
        /// Handles the Load event of the frm_Pinger control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Pinger_Load(object sender, EventArgs e) {

        }

        /// <summary>
        /// OT Server Connection State.
        /// </summary>
        private enum OTState { Unknow = 0, Online = 1, Offline = 2 }

        #region "[rgn] Form Properties "
        private Timer TMChk { get; set; }
        private Timer TMUpd { get; set; }
        private Timer TMLag { get; set; }

        private System.Threading.Timer TMPing { get; set; }
        private List<long> PingValues { get; set; }

        private Image StatusImage { get; set; }
        private string StatusText { get; set; }
        private int StatusPosition { get; set; }

        private int ErrorMargin { get { return 50; } }
        OTState ConnectionState { get; set; }
        private string LastOnlineServer { get; set; }

        private Thread ThreadCheck { get; set; }
        private DateTime StartConnectTime { get; set; }
        #endregion

        #region "[rgn] Control Events  "
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
        public long GetPingMs(string hostNameOrAddress) {
            var ping = new System.Net.NetworkInformation.Ping();
            return ping.Send(hostNameOrAddress, 1000, PingPacket()).RoundtripTime;
        }
        public byte[] PingPacket() {
            var packet = new byte[1000];
            for (int i = 0; i < packet.Length; i++) {
                packet[i] = 255;
            }
            return packet;
        }
        private void TMLag_Tick(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(LastOnlineServer)) {
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
            if (!string.IsNullOrEmpty(LastOnlineServer)) {
                var lag = GetPingMs(LastOnlineServer);
                if (lag > 0) { PingValues.Add(lag); }
            }
        }
        #endregion

        /// <summary>
        /// Checks this instance.
        /// </summary>
        public void Check() {

            TMPing.Dispose();
            PingValues = new List<long>();
            PingValues.Add(0);
            TMPing = new System.Threading.Timer(new TimerCallback(TMPing_Callback), null, 0, 1500);

            StatusImage = Properties.Resources.ball_yellow;
            StatusText = "Checking...";
            progressBarControl1.Position = 0;
            TMChk.Start();
            TMLag.Start();

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
                StartConnectTime = DateTime.Now;
                var session = new KTibiaX.IPChanger.Data.OTPinger.Session(txtIp.Text, txtPort.Text.ToInt32());
                session.Connect();

                Thread.Sleep(500);
                if (session.IsConnected) {
                    StatusText = "On Line!";
                    StatusImage = Properties.Resources.ball_green;
                    LastOnlineServer = txtIp.Text;
                }
                else {
                    StatusText = "Off Line!";
                    StatusImage = Properties.Resources.ball_red;
                    LastOnlineServer = "";
                }
                TMChk.Stop();
                StatusPosition = 100;
            }
            catch (SocketException) {
                MessageBox.Show("Server não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetErrorState();
            }
            catch (Exception) {
                SetErrorState();
                throw;
            }
        }

        /// <summary>
        /// Sets the state of the error.
        /// </summary>
        private void SetErrorState() {
            StatusText = "Off Line!";
            StatusImage = Properties.Resources.ball_red;
            TMChk.Stop();
            LastOnlineServer = "";
            StatusPosition = 100;
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