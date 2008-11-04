using System;
using System.Xml.Serialization;

namespace KTibiaX.IPChanger.Data {
    [Serializable]
    public class LoginServer {

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Ip { get; set; }

        [XmlAttribute]
        public int Port { get; set; }

        [XmlAttribute]
        public string Exp { get; set; }

        [XmlAttribute]
        public Version Version { get; set; }

    }
}
