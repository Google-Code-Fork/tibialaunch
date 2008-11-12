using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;

namespace KTibiaX.IPChanger.Features {
    public partial class frm_About : DevExpress.XtraEditors.XtraForm {
        public frm_About() {
            InitializeComponent();
        }

        private void frm_About_Load(object sender, EventArgs e) {
            lblVersion.Text = string.Concat(Program.GetCurrentResource().GetString("strVersion"),
                Assembly.GetExecutingAssembly().GetName().Version.ToString(), " (", 
                Assembly.GetExecutingAssembly().GetName().GetBuildDate().ToString("dd/MM/yyyy"), ")");
        }
    }
}