using System.Collections.Generic;
using System.Linq;

namespace Taggy.Model
{
    /// <summary>
    /// A cluster or set of tags.
    /// </summary>
    public class TagCluster
    {
        private List<Tag> items = new List<Tag>();

        public List<Tag> Items { get => items; set => items = value; }

        public Tag? GetTagByName(string name)
        {
            var item = items.FirstOrDefault(tag => tag.Name == name);
            if (item == null)
                return null;
            return item;
        }

        public Tag? GetTagByValue(string value)
        {
            var item = items.First(tag => tag.Value == value);
            if (item == null)
                return null;
            return item;
        }
    }
}
