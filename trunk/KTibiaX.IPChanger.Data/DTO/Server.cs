using System;
using System.Xml.Serialization;

namespace KTibiaX.IPChanger.Data.DTO {

    [Serializable, XmlRoot("tsqp")]
    public class Server {

        [XmlElement("serverinfo")]
        public ServerInfo Serverinfo { get; set; }

        [XmlElement("owner")]
        public Owner Owner { get; set; }

        [XmlElement("players")]
        public Players Players { get; set; }

        [XmlElement("monsters")]
        public Monsters Monsters { get; set; }

        [XmlElement("map")]
        public Map Map { get; set; }

        [XmlElement("motd")]
        public string Motd { get; set; }
    }
}
