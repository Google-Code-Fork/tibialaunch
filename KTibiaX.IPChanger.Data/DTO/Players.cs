using System;
using System.Xml.Serialization;

namespace KTibiaX.IPChanger.Data.DTO {

    [Serializable]
    public class Players {

        [XmlAttribute("online")]
        public string Online { get; set; }

        [XmlAttribute("max")]
        public string Max { get; set; }

        [XmlAttribute("peak")]
        public string Peak { get; set; }
    }
}
