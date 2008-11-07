using System;
using System.Xml.Serialization;

namespace KTibiaX.IPChanger.Data.DTO {

    [Serializable]
    public class Monsters {

        [XmlAttribute("total")]
        public string Total { get; set; }
    }
}
