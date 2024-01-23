using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Taggy.Model
{
    public static class TagCache
    {
        private static readonly Dictionary<string, Tag> tags = new Dictionary<string, Tag>();

        public static Tag GetTag(string name)
        {
            var key = GetKey("", name);
            if (!tags.ContainsKey(key))
                tags[key] = new Tag("", name);
            return tags[key];
        }

        public static Tag GetTag(string category, string name)
        {
            var key = GetKey(category, name);
            if (!tags.ContainsKey(key))
                tags[key] = new Tag(category, name);
            return tags[key];
        }

        public static Tag GetTag(Tag tag)
        {
            var key = GetKey(tag);
            if (!tags.ContainsKey(key))
                tags[key] = tag;
            return tags[key];
        }

        private static string GetKey(Tag tag)
        {
            return GetKey(tag.Category, tag.Name);
        }

        private static string GetKey(string category, string name)
        {
            return category + "=" + name;
        }
    }
}
