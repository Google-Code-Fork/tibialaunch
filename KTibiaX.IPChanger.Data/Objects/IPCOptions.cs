using System;
using System.Xml.Serialization;

namespace KTibiaX.IPChanger.Data {
    [Serializable]
    public class IPCOptions {

        [XmlElement]
        public bool ChangeFPS { get; set; }

        [XmlElement]
        public uint FPSValue { get; set; }

        [XmlElement]
        public bool IsolateMaps { get; set; }

        [XmlElement]
        public bool EnableMC { get; set; }

        [XmlElement]
        public bool WriteRSA { get; set; }

        [XmlElement]
        public string RSAKey { get; set; }
    }
}
