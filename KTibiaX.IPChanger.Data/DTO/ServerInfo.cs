using System;
using System.Xml.Serialization;

namespace KTibiaX.IPChanger.Data.DTO {
    [Serializable]
    public class ServerInfo {

        [XmlAttribute("uptime")]
        public string Uptime { get; set; }

        [XmlAttribute("ip")]
        public string Ip { get; set; }

        [XmlAttribute("servername")]
        public string Servername { get; set; }

        [XmlAttribute("port")]
        public string Port { get; set; }

        [XmlAttribute("location")]
        public string Location { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("server")]
        public string Server { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlAttribute("client")]
        public string Client { get; set; }
    }
}
