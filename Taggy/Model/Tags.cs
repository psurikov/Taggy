using System.Collections.Generic;
using System.Linq;

namespace Taggy.Model
{
    /// <summary>
    /// A cluster or set of tags.
    /// </summary>
    public class Tags
    {
        private List<Tag> items = new List<Tag>();

        public List<Tag> Items { get => items; set => items = value; }

        public Tag? GetTagByName(string name)
        {
            var matching = items.Where(tag => tag.Name == name);
            if (matching.Any() == false)
                return null;
            return matching.First();
        }

        public Tag? GetTagByValue(string value)
        {
            var matching = items.Where(tag => tag.Value == value);
            if (matching.Any() == false)
                return null;
            return matching.First();
        }

        public string? GetValue(string name)
        {
            var matching = items.Where(tag => tag.Name == name);
            if (matching.Any() == false)
                return null;
            return matching.First().Value;
        }

        public override string ToString()
        {
            var stringRepresentation = string.Join(", ", items);
            return stringRepresentation;
        }
    }
}
