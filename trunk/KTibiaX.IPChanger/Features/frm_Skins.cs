using System;
using DevExpress.Skins;
using KTibiaX.IPChanger.Properties;

namespace KTibiaX.IPChanger.Features {
    public partial class frm_Skins : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Skins"/> class.
        /// </summary>
        public frm_Skins() {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frm_Skins control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Skins_Load(object sender, EventArgs e) {
            foreach (SkinContainer cnt in SkinManager.Default.Skins) {
                lstSkins.Items.Add(cnt.SkinName);
                if (cnt.SkinName == Settings.Default.AppSkin) lstSkins.SelectedIndex = lstSkins.Items.Count - 1;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the lstSkins control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void lstSkins_SelectedIndexChanged(object sender, EventArgs e) {
            if (lstSkins.SelectedItem.ToString() != this.LookAndFeel.SkinName) {
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(lstSkins.SelectedItem.ToString());
                this.LookAndFeel.SetSkinStyle(lstSkins.SelectedItem.ToString());
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e) {
            Settings.Default.AppSkin = this.LookAndFeel.SkinName;
            Settings.Default.Save();
            Close();
        }
        
    }
}