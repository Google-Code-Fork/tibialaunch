using System;
using System.Windows.Forms;
using KTibiaX.IPChanger.Properties;
using System.Reflection;
using KTibiaX.IPChanger.Features;
using System.Resources;
using Keyrox.Shared.Objects;
using System.IO;
using IWshRuntimeLibrary;

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

            if (string.IsNullOrEmpty(Settings.Default.Culture)) { Application.Run(new frm_Culture()); }
            if (!string.IsNullOrEmpty(Settings.Default.Culture)) { Application.ExitThread(); }

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Settings.Default.Culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture(Settings.Default.Culture);

            CreateShortCut();
            Application.Run(new frm_StartClient());
        }

        /// <summary>
        /// The path used to load the application configuration.
        /// </summary>
        public static string ApplicationPath = Application.StartupPath;

        /// <summary>
        /// Gets the build date.
        /// </summary>
        /// <param name="asm">The asm.</param>
        /// <returns></returns>
        public static DateTime GetBuildDate(this AssemblyName asm) {
            return new System.DateTime(2000, 1, 1).AddDays(asm.Version.Build);
        }

        /// <summary>
        /// Gets the current resource.
        /// </summary>
        /// <returns></returns>
        public static ResourceManager GetCurrentResource() {
            return new ResourceManager("KTibiaX.IPChanger.AppLocal", typeof(frm_StartClient).Assembly);
        }

        /// <summary>
        /// Create the application shortcut if it does not exist yet.
        /// </summary>
        public static void CreateShortCut() {
            var WshShell = new WshShellClass();
            var linkDir = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "KTibiaX IPChanger.lnk"));

            if (!linkDir.Exists) {
                var MyShortcut = (IWshRuntimeLibrary.IWshShortcut)WshShell.CreateShortcut(linkDir.FullName);
                MyShortcut.WorkingDirectory = new FileInfo(Application.ExecutablePath).Directory.FullName;
                MyShortcut.TargetPath = Application.ExecutablePath;
                MyShortcut.Description = "KTibiaX IPChanger v2.10";
                MyShortcut.IconLocation = Path.Combine(new FileInfo(Application.ExecutablePath).Directory.FullName, "KTibiaX.IPChanger.ico");
                MyShortcut.Save();
            }
        }
    }
}
