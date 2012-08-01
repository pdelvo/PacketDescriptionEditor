using System.Collections;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace PacketDescriptionEditor.Helper
{
    public class DynamicXDocument : DynamicObject, IEnumerable
    {        
        private XElement _node;
               
        public DynamicXDocument(string fileName)
        {
            _node = (XElement)XDocument.Load(File.OpenRead(fileName)).FirstNode;
        }

        private DynamicXDocument(XElement node)
        {
            _node = node;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder.Name == _node.Name)
            {
                if (_node.HasElements)
                {
                    //Kein Endknoten!
                    result = this;
                }
                else
                {
                    result = _node.Value;
                }

                return true;
            }
            if (binder.Name == "Count")
            {
                result =  _node.Elements().Count();
                return true;
            }
            
            if (_node.HasAttributes)
            {
                if (_node.Attribute(binder.Name) != null)
                {
                    result = _node.Attribute(binder.Name).Value;
                    return true;
                }
            }

            if (_node.HasElements)
            {
                var node = from n in _node.Elements(binder.Name) select n;

                if (node.Count() == 0)
                {
                    result = null;
                    return false;
                }

                var element = node.First();

                if (element.HasElements)
                {
                    dynamic dynNode = new DynamicXDocument(element);
                    result = dynNode;
                }
                else
                {
                    result = element.Value;
                }

                return true;
            }
            
            result = null;
            return false;
        }        

        public IEnumerator GetEnumerator()
        {
            if (_node == null)
            {
                yield break;
            }

            foreach (var element in _node.Elements())
            {
                dynamic dynElement = new DynamicXDocument(element);
                yield return dynElement;
            }

        }
    }
}