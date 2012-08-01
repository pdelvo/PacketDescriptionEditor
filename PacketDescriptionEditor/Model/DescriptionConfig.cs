using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PacketDescriptionEditor.Model
{
    [Serializable]
    [XmlRoot("DataContext")]
    public class DescriptionConfig
    {
        [XmlArray(ElementName="Packets")]
        [XmlArrayItem("Packet")]
        public List<PacketConfig> Packets { get; set; }
    }
}
