using System;
using System.Reflection;
using System.Windows.Forms;
using KTibiaX.IPChanger.Properties;

namespace KTibiaX.IPChanger {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Settings.Default.AppSkin);
            Application.Run(new frm_StartClient());
        }

        /// <summary>
        /// Gets the build date.
        /// </summary>
        /// <param name="asm">The asm.</param>
        /// <returns></returns>
        public static DateTime GetBuildDate(this AssemblyName asm) {
            return new System.DateTime(2000, 1, 1).AddDays(asm.Version.Build);
        }
    }
}
