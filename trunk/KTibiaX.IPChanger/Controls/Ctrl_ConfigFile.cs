using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using KTibiaX.IPChanger.Data;
using KTibiaX.IPChanger.Features;
using KTibiaX.IPChanger.Properties;

namespace KTibiaX.IPChanger.Controls {
    public partial class Ctrl_ConfigFile : DevExpress.XtraEditors.XtraUserControl {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ctrl_ConfigFile"/> class.
        /// </summary>
        public Ctrl_ConfigFile() {
            InitializeComponent();
            var width = 0;
            foreach (LookUpColumnInfo col in ddlFiles.Properties.Columns) {
                width += col.Width;
            }
            ddlFiles.Properties.PopupWidth = width;
            ddlFiles.Properties.NullText = Program.GetCurrentResource().GetString("strDefault");
        }

        /// <summary>
        /// Handles the Load event of the Ctrl_ConfigFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Ctrl_ConfigFile_Load(object sender, EventArgs e) {
            RefreshDataSource();
        }

        #region "[rgn] Form Properties  "
        public TibiaCFGCollection DataSource { get; set; }
        private TibiaCFG currentConfigFile;
        public TibiaCFG CurrentConfigFile {
            get {
                if (ddlFiles.EditValue != null && !string.IsNullOrEmpty(ddlFiles.EditValue.ToString())) {
                    var configFiles = (from file in DataSource where file.Path == ddlFiles.EditValue.ToString() select file);
                    currentConfigFile = configFiles.Count() > 0 ? configFiles.First() : null;
                }
                return currentConfigFile;
            }
            set {
                currentConfigFile = value;
                if (value == null && currentConfigFile != null) ddlFiles.EditValue = null;
            }
        }
        #endregion

        #region "[rgn] Form Ctrl Events "
        private void ddlFiles_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            switch (e.Button.Kind) {
                case ButtonPredefines.Plus:

                    var frmTibiaConfig = new frm_TibiaConfig();
                    frmTibiaConfig.CFGSaved += new EventHandler<TibiaCFGEventArgs>(frmTibiaConfig_CFGSaved);
                    frmTibiaConfig.FormClosed += new FormClosedEventHandler(frmTibiaConfig_FormClosed);

                    this.ParentForm.Hide();
                    frmTibiaConfig.Show();
                    break;

                case ButtonPredefines.Delete:
                    if (ddlFiles.EditValue != null) {
                        var result = MessageBox.Show(Program.GetCurrentResource().GetString("strShure"), "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes) {

                            var file = new System.IO.FileInfo(CurrentConfigFile.Path);
                            if (file.Exists) file.Delete();

                            DataSource.Remove(CurrentConfigFile);
                            CurrentConfigFile = null;
                            ddlFiles.EditValue = null;

                            Settings.Default.ConfigFiles = DataSource;
                            Settings.Default.Save();
                            RefreshDataSource();
                        }
                    }
                    break;
            }
        }
        private void frmTibiaConfig_FormClosed(object sender, FormClosedEventArgs e) {
            this.ParentForm.Show();
        }
        private void frmTibiaConfig_CFGSaved(object sender, TibiaCFGEventArgs e) {
            RefreshDataSource();
            ddlFiles.EditValue = e.TibiaCFG.Path;
        }
        private void ddlFiles_EditValueChanging(object sender, ChangingEventArgs e) {
            if (e.NewValue == null) { CurrentConfigFile = null; }
            else {
                var files = from file in DataSource where file.Path == e.NewValue.ToString() select file;
                CurrentConfigFile = files.Count() > 0 ? files.First() : null;
            }
        }
        private void ddlFiles_TextChanged(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(ddlFiles.Text)) {
                ddlFiles.EditValue = null;
            }
        }
        private void ddlFiles_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) {
                ddlFiles.EditValue = null;
            }
        }
        #endregion

        /// <summary>
        /// Refreshes the data source.
        /// </summary>
        public void RefreshDataSource() {
            ddlFiles.Properties.BeginUpdate();
            DataSource = Settings.Default.ConfigFiles;
            ddlFiles.Properties.DataSource = DataSource;
            ddlFiles.Refresh();
            ddlFiles.Properties.EndUpdate();
        }
    }
}
