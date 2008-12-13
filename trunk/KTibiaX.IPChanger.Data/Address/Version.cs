using System;
using System.ComponentModel;

namespace KTibiaX.IPChanger.Data {

    [Serializable]
    public static class Versions { public const uint Total = 11; }

    public enum Version {

        [Description("7.6")]
        v760 = 0,

        [Description("7.92")]
        v792 = 1,

        [Description("8.0")]
        v800 = 2,

        [Description("8.1")]
        v810 = 3,

        [Description("8.11")]
        v811 = 4,

        [Description("8.2")]
        v820 = 5,

        [Description("8.21")]
        v821 = 6,

        [Description("8.22")]
        v822 = 7,

        [Description("8.3")]
        v830 = 8,

        [Description("8.31")]
        v831 = 9,
        
        [Description("8.4")]
        v84 = 10

    }
}
