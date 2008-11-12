using System;
using KTibiaX.IPChanger.Data.Objects;
using System.Windows.Forms;

namespace KTibiaX.IPChanger.Features {
    public partial class frm_UpdateInfo : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version"></param>
        public frm_UpdateInfo(SystemVersion version) {
            InitializeComponent();
            lblVersion.Text = version.Version;
            lblReleaseDate.Text = version.ReleaseDate.ToString("dd/MM/yyyy");
            txtNotes.Text = string.Format(version.UpdateDescription, Environment.NewLine);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_UpdateInfo_Load(object sender, EventArgs e) {
            txtNotes.SelectionStart = 1;
            txtNotes.SelectionLength = 1;
            btnDownload.Focus();
            txtNotes.Select(0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_Click(object sender, EventArgs e) {
            var url = Program.GetCurrentResource().GetString("strUrl");
            var info = new System.Diagnostics.ProcessStartInfo("iexplore.exe");
            info.Arguments = url;
            System.Diagnostics.Process.Start(info);
            Close();
        }

    }
}