using System;
using System.Xml.Serialization;

namespace KTibiaX.IPChanger.Data.DTO {
    [Serializable]
    public class Map {

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("author")]
        public string Author { get; set; }

        [XmlAttribute("width")]
        public string Width { get; set; }

        [XmlAttribute("height")]
        public string Height { get; set; }
    }
}
