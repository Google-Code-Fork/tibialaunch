using System;
using System.Xml.Serialization;

namespace KTibiaX.IPChanger.Data.Objects {
    [Serializable]
    public class SystemVersion {

        [XmlElement]
        public string Version { get; set; }

        [XmlElement]
        public DateTime ReleaseDate { get; set; }

        [XmlElement]
        public string UpdateDescription { get; set; }

    }
}
