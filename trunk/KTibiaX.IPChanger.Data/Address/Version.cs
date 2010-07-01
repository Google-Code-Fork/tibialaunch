using System;
using System.ComponentModel;
using System.Collections.Generic;
using Keyrox.Shared.Enumerators;

namespace KTibiaX.IPChanger.Data {

    [Serializable]
    public static class Versions {

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        public static Version GetVersion(string clientPath) {
            var items = Enumerators.GetEnumItems<Version>(true);
            var fileversion = System.Diagnostics.FileVersionInfo.GetVersionInfo(clientPath).ProductVersion;
            if (items.ContainsKey(fileversion)) {
                return items[fileversion];
            }
            return Version.Unknow;
        }
    }

    public enum Version {

        [Description("Unknow Version")]
        Unknow = 9999,

        [Description("7.60")]
        v760 = 0,

        [Description("7.92")]
        v792 = 1,

        [Description("8.00")]
        v800 = 2,

        [Description("8.10")]
        v810 = 3,

        [Description("8.11")]
        v811 = 4,

        [Description("8.20")]
        v820 = 5,

        [Description("8.21")]
        v821 = 6,

        [Description("8.22")]
        v822 = 7,

        [Description("8.30")]
        v830 = 8,

        [Description("8.31")]
        v831 = 9,

        [Description("8.40")]
        v84 = 10,

        [Description("8.41")]
        v841 = 11,
        
        [Description("8.42")]
        v842 = 12,

        [Description("8.50")]
        v850 = 121,

        [Description("8.54")]
        v854 = 13,

        [Description("8.55")]
        v855 = 14,

        [Description("8.57")]
        v857 = 15,

        [Description("8.60")]
        v860 = 16

    }

}
