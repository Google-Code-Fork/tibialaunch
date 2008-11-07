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
        }

        /// <summary>
        /// Occurs when [server change].
        /// </summary>
        public event EventHandler<LoginServerChangeEventArgs> ServerChange;

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e) {
            if (ddlVersion.SelectedIndex == 0 || ddlVersion.SelectedIndex == -1) {
                MessageBox.Show("Invalid Tibia Version!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var server = new LoginServer() {
                Exp = txtExp.Text,
                Ip = txtIP.Text.Trim(),
                Name = txtName.Text,
                Port = txtPort.Text.Trim().ToInt32(),
                Version = (Version)ddlVersion.Properties.Items[ddlVersion.SelectedIndex].Value.ToInt32()
            };
            var serverlist = Settings.Default.ServerList != null ? Settings.Default.ServerList : new LoginServerCollection();
            if ((from inserv in serverlist
                 where inserv.Ip.ToLower() == server.Ip.ToLower()
                 select inserv).Count() > 0) {
                MessageBox.Show("Server IP Already Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            serverlist.Add(server);
            Properties.Settings.Default.ServerList = serverlist;
            Properties.Settings.Default.Save();
            if (ServerChange != null) ServerChange(this, new LoginServerChangeEventArgs(server));
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
            combo.Properties.Items.Add(new ImageComboBoxItem("Select", -1, -1));
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