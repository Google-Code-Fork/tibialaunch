using System;
using System.Globalization;
using System.Threading;
using KTibiaX.IPChanger.Properties;

namespace KTibiaX.IPChanger.Features {
    public partial class frm_Culture : DevExpress.XtraEditors.XtraForm {
        public frm_Culture() {
            InitializeComponent();
        }

        private void frm_Culture_Load(object sender, EventArgs e) {
            switch (ddlCulture.SelectedIndex) {
                case 0: SetEnglish(); break;
                case 1: SetPortuguese(); break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e) {
            Close();
        }

        private void ddlCulture_SelectedIndexChanged(object sender, EventArgs e) {
            switch (ddlCulture.SelectedIndex) {
                case 0: SetEnglish(); break;
                case 1: SetPortuguese(); break;
            }
            Settings.Default.Save();
        }

        private void SetEnglish() {
            Settings.Default.Culture = "en-US";
            imgCulture.Image = Properties.Resources.USA;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");
            layoutddl.Text = Program.GetCurrentResource().GetString("strLanguage");
            this.Update(); this.Refresh();
        }

        private void SetPortuguese() {
            Settings.Default.Culture = "pt-BR";
            imgCulture.Image = Properties.Resources.Brazil;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("pt-BR");
            layoutddl.Text = Program.GetCurrentResource().GetString("strLanguage");
            this.Update(); this.Refresh();
        }
    }
}