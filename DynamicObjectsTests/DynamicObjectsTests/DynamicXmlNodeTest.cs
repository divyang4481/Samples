using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace DynamicObjectsTests
{
    public class DynamicXmlNodeTest
    {
        [Fact]
        public void ShouldConvertDynamicToXElementObject()
        {
            dynamic contact = new DynamicXmlNode("Contacts");

            contact.Name = "Some Name";
            contact.Phone = "206-555-0144";
            contact.Address = new DynamicXmlNode();
            contact.Address.Street = "123 Main St";
            contact.Address.City = "Mercer Island";
            contact.Address.State = "WA";
            contact.Address.Postal = "68402";

            var xElementContact = (XElement) contact;

            Assert.NotNull(xElementContact);
            Assert.Equal("Contacts", xElementContact.Name);
        }
    }
}
