using System;
using System.Linq;
using DevExpress.XtraEditors.Controls;
using KTibiaX.IPChanger.Data;
using KTibiaX.IPChanger.Properties;
using KTibiaX.IPChanger.Features;
using System.Windows.Forms;

namespace KTibiaX.IPChanger.Controls {
    public partial class Ctrl_ServerList : DevExpress.XtraEditors.XtraUserControl {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ctrl_ServerList"/> class.
        /// </summary>
        public Ctrl_ServerList() {
            InitializeComponent();
            var width = 0;
            foreach (LookUpColumnInfo col in lookUpEdit1.Properties.Columns) {
                width += col.Width;
            }
            lookUpEdit1.Properties.PopupWidth = width;
            lookUpEdit1.Properties.NullText = Program.GetCurrentResource().GetString("strSelectServer");
        }

        /// <summary>
        /// Handles the Load event of the Ctrl_ServerList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Ctrl_ServerList_Load(object sender, EventArgs e) {
            RefreshDataSource();
        }

        #region "[rgn] Control Properties "
        public LoginServerCollection DataSource { get; set; }
        public LoginServer CurrentServer { get; set; }
        public string CurrentIP { get { return lookUpEdit1.EditValue != null ? lookUpEdit1.EditValue.ToString() : ""; } }
        public event EventHandler<LoginServerChangeEventArgs> ServerChange;
        #endregion

        #region "[rgn] Control Events     "
        private void lookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            switch (e.Button.Kind) {
                case DevExpress.XtraEditors.Controls.ButtonPredefines.Close:
                    if (CurrentServer != null) {
                        var result =
                            MessageBox.Show(Program.GetCurrentResource().GetString("strDeleteServer"), "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes) {
                            DataSource.Remove(CurrentServer);
                            Settings.Default.ServerList = DataSource;
                            Settings.Default.Save();
                        }
                    }
                    break;
                case DevExpress.XtraEditors.Controls.ButtonPredefines.Plus:
                    var frmServer = new frm_Server();
                    frmServer.ServerChange += new EventHandler<LoginServerChangeEventArgs>(frmServer_ServerChange);
                    frmServer.FormClosed += new System.Windows.Forms.FormClosedEventHandler(frmServer_FormClosed);
                    this.ParentForm.Hide();
                    frmServer.Show();
                    break;
            }
        }
        private void frmServer_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e) {
            this.ParentForm.Show();
        }
        private void frmServer_ServerChange(object sender, LoginServerChangeEventArgs e) {
            lookUpEdit1.Properties.BeginUpdate();
            DataSource = Settings.Default.ServerList;
            lookUpEdit1.Properties.DataSource = DataSource;
            lookUpEdit1.Properties.EndUpdate();
            LoadServer(e.LoginServer.Ip);
        }
        private void lookUpEdit1_TextChanged(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(lookUpEdit1.Text)) {
                lookUpEdit1.EditValue = null;
            }
        }
        private void lookUpEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e) {
            if (e.NewValue == null) { CurrentServer = null; DisableButton(ButtonPredefines.Close); }
            else {
                var servers = (from server in DataSource where server.Ip == e.NewValue.ToString() select server);
                if (servers.Count() > 0) {
                    CurrentServer = servers.First();
                    EnableButton(ButtonPredefines.Close);
                }
            }
            if (ServerChange != null) ServerChange(this, new LoginServerChangeEventArgs(CurrentServer));
        }
        private void lookUpEdit1_KeyPress(object sender, KeyPressEventArgs e) {
            if (lookUpEdit1.Text != "") {
                lookUpEdit1.EditValue = lookUpEdit1.Text;
            }
            else { lookUpEdit1.EditValue = null; }
        }
        private void lookUpEdit1_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) {
                lookUpEdit1.EditValue = null;
            }
        }
        #endregion

        /// <summary>
        /// Refreshes the data source.
        /// </summary>
        public void RefreshDataSource() {
            lookUpEdit1.Properties.BeginUpdate();
            DataSource = Settings.Default.ServerList;
            lookUpEdit1.Properties.DataSource = DataSource;
            lookUpEdit1.Refresh();
            lookUpEdit1.Properties.EndUpdate();
        }

        /// <summary>
        /// Disables the button.
        /// </summary>
        /// <param name="kind">The button kind.</param>
        public void DisableButton(ButtonPredefines kind) {
            foreach (EditorButton btn in lookUpEdit1.Properties.Buttons) {
                if (btn.Kind == kind) continue;
                btn.Enabled = false; return;
            }
        }

        /// <summary>
        /// Loads the server.
        /// </summary>
        /// <param name="ip">The ip.</param>
        public void LoadServer(string ip) {
            if (DataSource == null && !string.IsNullOrEmpty(ip)) {
                lookUpEdit1.EditValue = null;
                lookUpEdit1.Text = ip;
            }
            else if (DataSource != null && !string.IsNullOrEmpty(ip)) {
                var clients = (from inClient in DataSource where inClient.Ip == ip select inClient);
                if (clients.Count() > 0) {
                    lookUpEdit1.EditValue = clients.First().Ip;
                }
                else {
                    lookUpEdit1.EditValue = ip;
                    lookUpEdit1.Text = ip;
                }
            }
            else if (string.IsNullOrEmpty(ip)) {
                lookUpEdit1.EditValue = null;
            }
        }

        /// <summary>
        /// Enables the button.
        /// </summary>
        /// <param name="kind">The button kind.</param>
        public void EnableButton(ButtonPredefines kind) {
            foreach (EditorButton btn in lookUpEdit1.Properties.Buttons) {
                if (btn.Kind == kind) continue;
                btn.Enabled = true; return;
            }
        }
    }
}
