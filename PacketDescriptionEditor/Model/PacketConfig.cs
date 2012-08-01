using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PacketDescriptionEditor.Model
{
    [Serializable]
    public class PacketConfig
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [XmlArray(ElementName = "Fields")]
        [XmlArrayItem("Field")]
        public List<FieldConfig> Fields { get; set; }

        public string Size { get; set; }
    }
}
