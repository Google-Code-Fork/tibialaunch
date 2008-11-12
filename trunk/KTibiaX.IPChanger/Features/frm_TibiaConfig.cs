using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using KTibiaX.IPChanger.Data;
using KTibiaX.IPChanger.Data.DTO;
using KTibiaX.IPChanger.Properties;
using KTibiaX.Shared.Enumerators;
using KTibiaX.Shared.Objects;
using Version = KTibiaX.IPChanger.Data.Version;

namespace KTibiaX.IPChanger.Features {
    public partial class frm_TibiaConfig : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_TibiaConfig"/> class.
        /// </summary>
        public frm_TibiaConfig() {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frm_TibiaConfig control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_TibiaConfig_Load(object sender, EventArgs e) {
            AddEnumItems(ddlVersion, typeof(Version), true);
            AddEnumItems(ddlVocation, typeof(Vocation), false);
        }

        /// <summary>
        /// Gets or sets the current files.
        /// </summary>
        /// <value>The current files.</value>
        public TibiaCFGCollection CurrentFiles {
            get {
                return Settings.Default.ConfigFiles != null ?
                    Settings.Default.ConfigFiles :
                    new TibiaCFGCollection();
            }
            set {
                Settings.Default.ConfigFiles = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Occurs when [CFG saved].
        /// </summary>
        public event EventHandler<TibiaCFGEventArgs> CFGSaved;

        /// <summary>
        /// Add All Enum Items on defined Image Combo Box.
        /// </summary>
        /// <param name="combo">Combo Box to add items.</param>
        /// <param name="enumType">Enum to get items.</param>
        public void AddEnumItems(ImageComboBoxEdit combo, Type enumType, bool select) {
            combo.Properties.Items.BeginUpdate();
            if (select)
                combo.Properties.Items.Add(new ImageComboBoxItem(string.Concat("[", Program.GetCurrentResource().GetString("strSelect"), "]"), -1, -1));
            Array items = Enum.GetValues(enumType);
            foreach (Enum item in items) {
                string enumText = item.Description() != "" ? item.Description() : item.ToString();
                combo.Properties.Items.Add(new ImageComboBoxItem(enumText, item.GetHashCode(), -1));
            }
            combo.Properties.Items.EndUpdate();
            combo.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e) {

            #region "[rgn] Validation "
            if (txtDesc.Text.Length < 3) { dxErrorProvider1.SetError(txtDesc, Program.GetCurrentResource().GetString("strInvalidDescription")); return; }
            else { dxErrorProvider1.SetError(txtDesc, ""); }
            if (txtFile.Text.Length < 3) { dxErrorProvider1.SetError(txtFile, Program.GetCurrentResource().GetString("strConfigFileNotFoundfrm")); return; }
            else { dxErrorProvider1.SetError(txtFile, ""); }
            if (ddlVersion.SelectedIndex == 0) { dxErrorProvider1.SetError(ddlVersion, Program.GetCurrentResource().GetString("strInvalidVersion")); return; }
            else { dxErrorProvider1.SetError(ddlVersion, ""); }
            #endregion

            //Copy Config File.
            var file = new FileInfo(txtFile.Text);
            if (!file.Exists) {
                MessageBox.Show(Program.GetCurrentResource().GetString("strConfigFileNotFoundfrm"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var configDir = new DirectoryInfo(string.Concat(Application.StartupPath, "\\",Settings.Default.ConfigFilesDir));
            if(!configDir.Exists) configDir.Create();
            var localFile = string.Concat(configDir, "\\Tibia", DateTime.Now.ToOADate().ToString().Replace(".",""), ".cfg");
            file.CopyTo(localFile);

            //Save Config Record.
            var configfile = new TibiaCFG() {
                Description = txtDesc.Text,
                Path = localFile,
                Version = (Version)ddlVersion.Properties.Items[ddlVersion.SelectedIndex].Value.ToInt32(),
                Vocation = (Vocation)ddlVocation.Properties.Items[ddlVocation.SelectedIndex].Value.ToInt32()
            };

            //Update and Close.
            var files = CurrentFiles;
            files.Add(configfile);
            CurrentFiles = files;
            if (CFGSaved != null) CFGSaved(this, new TibiaCFGEventArgs(configfile));
            Close();
        }

        /// <summary>
        /// Handles the ButtonClick event of the txtFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
        private void txtFile_ButtonClick(object sender, ButtonPressedEventArgs e) {
            openFileDialog1.ShowDialog();
            if (!string.IsNullOrEmpty(openFileDialog1.FileName))
                txtFile.Text = openFileDialog1.FileName;
        }

    }
}