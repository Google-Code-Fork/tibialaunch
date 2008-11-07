using System;
using System.Xml.Serialization;

namespace KTibiaX.IPChanger.Data.DTO {

    [Serializable]
    public class Owner {

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("email")]
        public string Email { get; set; }
    }
}
