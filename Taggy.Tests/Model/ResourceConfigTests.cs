using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Taggy.Model;

namespace Taggy.Tests.Model
{
    [TestClass]
    public class ResourceConfigTests
    {
        [TestMethod]
        public void LoadXml()
        {
            var xml = "<Resources>\r\n" +
            "  <Resource>\r\n" +
            "    <Location>https://host</Location>\r\n" +
            "    <Tags>@(Tag1=Value)</Tags>\r\n" +
            "  </Resource>\r\n" +
            "  <Resource>\r\n" +
            "    <Location>https://host2</Location>\r\n" +
            "    <Tags>@(Tag2=Value)</Tags>\r\n" +
            "  </Resource>\r\n" +
            "</Resources>";

            var resourceConfig = new ResourceConfig();
            resourceConfig.LoadXml(xml);
            var resources = resourceConfig.Resources.ToList();
            Assert.IsTrue(resources[0].Location == "https://host");
            Assert.IsTrue(resources[0].Tags.GetValue("Tag1") == "Value");
            Assert.IsTrue(resources[1].Location == "https://host2");
            Assert.IsTrue(resources[1].Tags.GetValue("Tag2") == "Value");
        }

        [TestMethod]
        public void SetResources()
        {
            var tags = new Tags();
            tags.Items.Add(new Tag("Name", "Value"));
            var resource1 = new Resource("http://host", tags);
            var resources = new List<Resource>();
            resources.Add(resource1);
            var resourceConfig = new ResourceConfig();
            resourceConfig.Resources = resources;

            var stringBuilder = new StringBuilder();
            resourceConfig.Save(stringBuilder);
            var xml = stringBuilder.ToString();
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            Assert.IsTrue(xmlDocument.SelectSingleNode("Resources/Resource/Location")?.InnerText == "http://host");
            Assert.IsTrue(xmlDocument.SelectSingleNode("Resources/Resource/Tags")?.InnerText == "@(Name=Value)");
        }
    }
}
