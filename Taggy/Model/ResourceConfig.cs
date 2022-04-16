using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Taggy.Model
{
    public class ResourceConfig
    {
        private XmlDocument xmlDocument;

        public ResourceConfig()
        {
            xmlDocument = new XmlDocument();
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
                    var resource = new Resource(location, TagsConverter.FromString(tagsString));
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
                }
            }
        }

        public void Save(string filePath)
        {
            xmlDocument.Save(filePath);
        }

        private static XmlNode AddNode(XmlNode parentNode, string name)
        {
            return AddNode(parentNode, name, "");
        }

        private static XmlNode AddNode(XmlNode parentNode, string name, string value)
        {
            var ownerDocument = parentNode.OwnerDocument;
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
