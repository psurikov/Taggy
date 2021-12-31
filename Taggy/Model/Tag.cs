using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taggy.Model
{
    /// <summary>
    /// Tag is a descriptive token that can be applied to any electronic document. For example, the book may have an author=Rudyard Kipling
    /// </summary>
    public class Tag
    {
        private string name = "";
        private string value = "";

        public Tag(string value)
        {
            Name = "";
            Value = value;
        }

        public Tag(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get => name; set => name = value; }
        public string Value { get => value; set => this.value = value; }
    }
}
