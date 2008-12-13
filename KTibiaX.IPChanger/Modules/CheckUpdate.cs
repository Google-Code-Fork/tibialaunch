using System;
using System.IO;
using System.Net;
using System.Reflection;
using KTibiaX.IPChanger.Data;
using KTibiaX.IPChanger.Data.Objects;
using KTibiaX.IPChanger.Properties;
using KTibiaX.Shared.Objects;
using System.Threading;
using System.Globalization;

namespace KTibiaX.IPChanger.Modules {
    public class CheckUpdate {

        /// <summary>
        /// 
        /// </summary>
        public void BeginCheckVersion() {
            var thCheck = new Thread(CheckVersion);
            thCheck.IsBackground = true;
            thCheck.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckVersion() {
            try {
                var sysver = GetLastVersion();
                if (sysver.ReleaseDate > Program.GetBuildDate(Assembly.GetExecutingAssembly().GetName())) {
                    if (NewVersionDetected != null) NewVersionDetected(this, new SystemVersionEventArgs(sysver));
                }
                else if (NoUpdateAvailable != null) NoUpdateAvailable(this, EventArgs.Empty);
            }
            catch (WebException) { if (WebNotAvailable != null) WebNotAvailable(this, EventArgs.Empty); }
        }

        #region "[rgn] Module State Changed Events  "
        public event EventHandler WebNotAvailable;
        public event EventHandler NoUpdateAvailable;
        public event EventHandler<SystemVersionEventArgs> NewVersionDetected;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private SystemVersion GetLastVersion() {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Properties.Settings.Default.Culture);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(Properties.Settings.Default.Culture);

            var netClient = new WebClient();
            Stream strm = netClient.OpenRead(Program.GetCurrentResource().GetString("strUpdateURL"));
            StreamReader sr = new StreamReader(strm);
            var xml = sr.ReadToEnd(); strm.Close();
            return xml.Deserialize<SystemVersion>();
        }
    }
}
