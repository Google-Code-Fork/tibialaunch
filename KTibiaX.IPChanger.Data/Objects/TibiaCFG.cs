using System;
using System.Xml.Serialization;
using KTibiaX.IPChanger.Data.DTO;

namespace KTibiaX.IPChanger.Data {
    [Serializable]
    public class TibiaCFG {

        [XmlElement]
        public string Description { get; set; }

        [XmlElement]
        public string Path { get; set; }

        [XmlElement]
        public Version Version { get; set; }

        [XmlElement]
        public Vocation Vocation { get; set; }
        
    }
}
