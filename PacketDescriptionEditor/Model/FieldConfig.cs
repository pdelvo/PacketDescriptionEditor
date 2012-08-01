using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PacketDescriptionEditor.Model
{
    [Serializable]
    public class FieldConfig
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Example { get; set; }
        [XmlElement("Notes")]
        public string Note { get; set; }
    }
}
