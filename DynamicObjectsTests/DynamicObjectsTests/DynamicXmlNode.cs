using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DynamicObjectsTests
{
    public class DynamicXmlNode : DynamicObject
    {
        private XElement node;

        public DynamicXmlNode()
        {
            
        }

        public DynamicXmlNode(XElement node)
        {
            this.node = node;
        }

        public DynamicXmlNode(string name)
        {
            this.node = new XElement(name);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            XElement setNode = node.Element(binder.Name);
            if (setNode != null)
                setNode.SetValue(value);
            else
            {
                node.Add(value.GetType() == typeof (DynamicXmlNode)
                             ? new XElement(binder.Name)
                             : new XElement(binder.Name, value));
            }
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            XElement getNode = node.Element(binder.Name);
            if (getNode != null)
            {
                result = new DynamicXmlNode(getNode);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type == typeof(string))
            {
                result = node.Value;
                return true;
            }
            else if (binder.Type == typeof (XElement))
            {
                result = node;
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        // Access to methods
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            Type xmlType = typeof(XElement);
            try
            {
                result = xmlType.InvokeMember(
                          binder.Name,
                          BindingFlags.InvokeMethod |
                          BindingFlags.Public |
                          BindingFlags.Instance,
                          null, node, args);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}
