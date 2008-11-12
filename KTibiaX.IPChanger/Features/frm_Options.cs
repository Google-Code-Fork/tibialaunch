using System;
using System.Windows.Forms;
using KTibiaX.IPChanger.Properties;
using KTibiaX.IPChanger.Data;

namespace KTibiaX.IPChanger.Features {
    public partial class frm_Options : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Options"/> class.
        /// </summary>
        public frm_Options() {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frm_Options control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Options_Load(object sender, EventArgs e) {
            LoadData();
        }

        #region "[rgn] Form Properties "
        private IPCOptions Options { get; set; }
        public event EventHandler<IPCOptionsEventArgs> OptionsChanged;
        #endregion

        #region "[rgn] Control Events  "
        private void txtMapPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            if (txtMapPath.Text != "")
                folderBrowserDialog1.SelectedPath = txtMapPath.Text;
            folderBrowserDialog1.ShowDialog();
            txtMapPath.Text = folderBrowserDialog1.SelectedPath;
        }
        private void ckFPs_CheckedChanged(object sender, EventArgs e) {
            txtFPS.Enabled = ckFPs.Checked;
        }
        private void ckGraphics_CheckedChanged(object sender, EventArgs e) {
            ddlGraphics.Enabled = ckGraphics.Checked;
        }
        private void ckRSA_CheckedChanged(object sender, EventArgs e) {
            txtRSA.Enabled = ckRSA.Checked;
        }
        private void btnReset_Click(object sender, EventArgs e) {
            ResetData();
        }
        private void btnSave_Click(object sender, EventArgs e) {
            SaveData();
        }
        #endregion

        /// <summary>
        /// Loads this instance.
        /// </summary>
        private void LoadData() {
            ckFPs.Checked = Settings.Default.ChangeFPS;
            txtFPS.Text = Settings.Default.FPSValue.ToString();
            ckGraphics.Checked = Settings.Default.ChangeGraphics;
            ddlGraphics.SelectedIndex = 0;
            if (Settings.Default.GraphicsEngine != "") {
                foreach (var item in ddlGraphics.Properties.Items) {
                    if (item.ToString() == Settings.Default.GraphicsEngine)
                        ddlGraphics.SelectedItem = item;
                }
            }
            ckMaps.Checked = Settings.Default.DistinctMaps;
            ckMC.Checked = Settings.Default.EnableMC;
            ckRSA.Checked = Settings.Default.WriteRSA;
            txtRSA.Text = Settings.Default.RSAKey;
            ckClose.Checked = Settings.Default.CloseAfterStart;
            if (string.IsNullOrEmpty(Settings.Default.OTMapPath)) {
                txtMapPath.Text = string.Concat(Environment.CurrentDirectory, "\\OTServMaps\\");
            }
            else { txtMapPath.Text = Settings.Default.OTMapPath; }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        private void SaveData() {
            Settings.Default.ChangeFPS = ckFPs.Checked;
            Settings.Default.FPSValue = Convert.ToUInt32(txtFPS.Text);
            Settings.Default.ChangeGraphics = ckGraphics.Checked;
            Settings.Default.GraphicsEngine = ddlGraphics.SelectedItem != null ? ddlGraphics.SelectedItem.ToString() : "";
            Settings.Default.DistinctMaps = ckMaps.Checked;
            Settings.Default.EnableMC = ckMC.Checked;
            Settings.Default.WriteRSA = ckRSA.Checked;
            Settings.Default.RSAKey = txtRSA.Text;
            Settings.Default.OTMapPath = txtMapPath.Text;
            Settings.Default.CloseAfterStart = ckClose.Checked;
            Settings.Default.Save();
            Close();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        private void ResetData() {
            ckFPs.Checked = false;
            txtFPS.Text = "0";
            ckGraphics.Checked = false;
            ddlGraphics.SelectedIndex = 3;
            ckMaps.Checked = true;
            ckMC.Checked = true;
            ckRSA.Checked = true;
            ckClose.Checked = true;
            txtMapPath.Text = string.Concat(Environment.CurrentDirectory, "\\OTServMaps\\");
            txtRSA.Text = "109120132967399429278860960508995541528237502902798129123468757937266291492576446330739696001110603907230888610072655818825358503429057592827629436413108566029093628212635953836686562675849720620786279431090218017681061521755056710823876476444260558147179707119674283982419152118103759076030616683978566631413";
            SaveData();
        }

        /// <summary>
        /// Handles the FormClosing event of the frm_Options control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void frm_Options_FormClosing(object sender, FormClosingEventArgs e) {
            if (OptionsChanged != null) {
                OptionsChanged(this, new IPCOptionsEventArgs(
                    new IPCOptions() {
                        ChangeFPS = Settings.Default.ChangeFPS,
                        FPSValue = Settings.Default.FPSValue,
                        IsolateMaps = Settings.Default.DistinctMaps,
                        WriteRSA = Settings.Default.WriteRSA,
                        EnableMC = Settings.Default.EnableMC,
                        RSAKey = Settings.Default.RSAKey
                    }
                ));
            }
        }

    }
}