using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Taggy.Model
{
    public class ResourceConfig
    {
        private readonly XmlDocument xmlDocument;

        public ResourceConfig()
        {
            xmlDocument = new XmlDocument();
        }

        public void LoadFile(string filePath)
        {
            xmlDocument.Load(filePath);
        }

        public void LoadXml(string xml)
        {
            xmlDocument.LoadXml(xml);
        }

        public IEnumerable<Resource> Resources
        {
            get
            {
                var resources = new List<Resource>();
                var resourcesNodes = new List<XmlNode>();
                var selectedNodes = xmlDocument.SelectNodes("/Resources/Resource");
                if (selectedNodes != null)
                    resourcesNodes = selectedNodes.OfType<XmlNode>().ToList();

                foreach (var resourceNode in resourcesNodes)
                {
                    var location = GetValue(resourceNode, "Location") ?? "";
                    var tagsString = GetValue(resourceNode, "Tags") ?? "";
                    var dateAdded = new DateTime(Convert.ToInt32(GetValue(resourceNode, "DateAdded")));
                    var resource = new Resource(location, TagsConverter.FromString(tagsString), dateAdded);
                    resources.Add(resource);
                }

                return resources;
            }
            set
            {
                if (xmlDocument.DocumentElement != null)
                    xmlDocument.RemoveChild(xmlDocument.DocumentElement);
                var rootNode = AddNode(xmlDocument, "Resources");
                foreach (var resource in value)
                {
                    var resourceNode = AddNode(rootNode, "Resource");
                    AddNode(resourceNode, "Location", resource.Location);
                    AddNode(resourceNode, "Tags", TagsConverter.ToString(resource.Tags));
                    AddNode(resourceNode, "DateAdded", resource.DateAdded.Ticks.ToString());
                }
            }
        }

        public void Save(string filePath)
        {
            xmlDocument.Save(filePath);
        }

        public void Save(StringBuilder stringBuilder)
        {
            var memoryStream = new MemoryStream();
            xmlDocument.Save(memoryStream);
            var bytes = memoryStream.ToArray();
            var encoding = Encoding.UTF8;
            var str = encoding.GetString(bytes);
            stringBuilder.AppendLine(str);
        }

        private static XmlNode AddNode(XmlNode parentNode, string name)
        {
            return AddNode(parentNode, name, "");
        }

        private static XmlNode AddNode(XmlNode parentNode, string name, string value)
        {
            var ownerDocument = parentNode.OwnerDocument;
            if (ownerDocument == null)
                ownerDocument = parentNode as XmlDocument;
            if (ownerDocument == null)
                throw new Exception("The node does not specify the owner document.");
            var element = ownerDocument.CreateElement(name);
            var addedNode = parentNode.AppendChild(element);
            if (addedNode == null)
                throw new Exception("The child node has not been created.");
            if (!string.IsNullOrEmpty(value))
                addedNode.InnerText = value;
            return addedNode;
        }

        private static string? GetValue(XmlNode node, string xpath)
        {
            var resultNode = node.SelectSingleNode(xpath);
            if (resultNode == null)
                return null;
            return resultNode.InnerText;
        }
    }
}
