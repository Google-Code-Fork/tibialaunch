using System;
using System.ComponentModel;

namespace KTibiaX.IPChanger.Data.DTO {
    public enum Vocation {

        [Description("[All Vocations]")]
        All = 0,

        [Description("Knights")]
        Knight = 1,

        [Description("Paladins")]
        Paladin = 2,

        [Description("Druids")]
        Druid = 3,

        [Description("Sorcerers")]
        Sorcerer = 4

    }
}
