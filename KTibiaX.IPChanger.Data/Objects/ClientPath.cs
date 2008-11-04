using System;
using System.Xml.Serialization;

namespace KTibiaX.IPChanger.Data {
    [Serializable]
    public class ClientPath {

        [XmlAttribute]
        public string Path { get; set; }

        [XmlAttribute]
        public Version Version { get; set; }

    }
}
