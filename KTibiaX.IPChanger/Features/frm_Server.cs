using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KTibiaX.Shared.Enumerators;
using DevExpress.XtraEditors.Controls;
using KTibiaX.IPChanger.Data;
using KTibiaX.Shared.Objects;
using Version = KTibiaX.IPChanger.Data.Version;
using KTibiaX.IPChanger.Properties;

namespace KTibiaX.IPChanger.Features {
    public partial class frm_Server : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Server"/> class.
        /// </summary>
        public frm_Server() {
            InitializeComponent();
            AddEnumItems(ddlVersion, typeof(Version));
            CurrentServerIndex = -1;
        }

        /// <summary>
        /// Occurs when [server change].
        /// </summary>
        public event EventHandler<LoginServerChangeEventArgs> ServerChange;

        /// <summary>
        /// Gets or sets the current server.
        /// </summary>
        /// <value>The current server.</value>
        public LoginServer CurrentServer { get; set; }

        /// <summary>
        /// Gets or sets the index of the current server.
        /// </summary>
        /// <value>The index of the current server.</value>
        public int CurrentServerIndex { get; set; }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e) {
            if (ddlVersion.SelectedIndex == 0 || ddlVersion.SelectedIndex == -1) {
                MessageBox.Show(Program.GetCurrentResource().GetString("strInvalidVersion"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (CurrentServer == null) { CurrentServer = new LoginServer(); }
            CurrentServer.Exp = txtExp.Text;
            CurrentServer.Ip = txtIP.Text.Trim();
            CurrentServer.Name = txtName.Text;
            CurrentServer.Port = txtPort.Text.Trim().ToInt32();
            CurrentServer.Version = (Version)ddlVersion.Properties.Items[ddlVersion.SelectedIndex].Value.ToInt32();

            var serverlist = Settings.Default.ServerList != null ? Settings.Default.ServerList : new LoginServerCollection();
            var serverWithIP = (from inserv in serverlist where inserv.Ip.ToLower() == txtIP.Text.Trim().ToLower() && inserv.Name != txtName.Text select inserv);

            if (serverWithIP.Count() > 0) {
                foreach (var innerserver in serverWithIP) {
                    if (innerserver.Port == txtPort.Text.Trim().ToInt32()) {
                        MessageBox.Show(Program.GetCurrentResource().GetString("strIPExist"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            if (CurrentServerIndex == -1) { serverlist.Add(CurrentServer); } else { serverlist[CurrentServerIndex] = CurrentServer; }
            Properties.Settings.Default.ServerList = serverlist;
            Properties.Settings.Default.Save();
            if (ServerChange != null) ServerChange(this, new LoginServerChangeEventArgs(CurrentServer));
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnStatus_Click(object sender, EventArgs e) {
            var frmChecker = new frm_Pinger(txtIP.Text, txtPort.Text);
            frmChecker.FormClosed += new FormClosedEventHandler(frmChecker_FormClosed);
            this.Hide();
            frmChecker.Show();
        }

        /// <summary>
        /// Loads the server.
        /// </summary>
        public void LoadServer(LoginServer server, int index) {
            CurrentServer = server;
            CurrentServerIndex = index;
            txtName.Text = CurrentServer.Name;
            txtPort.Text = CurrentServer.Port.ToString();
            txtExp.Text = CurrentServer.Exp.ToString();
            txtIP.Text = CurrentServer.Ip.ToString();
            ddlVersion.EditValue = CurrentServer.Version.GetHashCode();
        }

        /// <summary>
        /// Handles the FormClosed event of the frmChecker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.</param>
        private void frmChecker_FormClosed(object sender, FormClosedEventArgs e) {
            this.Show();
        }

        /// <summary>
        /// Add All Enum Items on defined Image Combo Box.
        /// </summary>
        /// <param name="combo">Combo Box to add items.</param>
        /// <param name="enumType">Enum to get items.</param>
        public void AddEnumItems(ImageComboBoxEdit combo, Type enumType) {
            combo.Properties.Items.BeginUpdate();
            combo.Properties.Items.Add(new ImageComboBoxItem(Program.GetCurrentResource().GetString("strSelect"), -1, -1));
            Array items = Enum.GetValues(enumType);
            foreach (Enum item in items) {
                string enumText = item.Description() != "" ? item.Description() : item.ToString();
                combo.Properties.Items.Add(new ImageComboBoxItem(enumText, item.GetHashCode(), -1));
            }
            combo.Properties.Items.EndUpdate();
            combo.SelectedIndex = 0;
        }
    }
}