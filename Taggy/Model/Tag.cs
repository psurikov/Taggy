using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taggy.Model
{
    /// <summary>
    /// Tag is a descriptive token that can be applied to any resource. For example, the book may have an author=Rudyard Kipling
    /// </summary>
    public class Tag
    {
        private string category = "";
        private string name = "";
        private double relevant = 1d;

        public Tag()
        {
            Category = "";
            Name = "";
        }

        public Tag(string name)
        {
            Category = "";
            Name = name;
        }

        public Tag(string category, string name)
        {
            Category = category;
            Name = name;
        }

        public string Category { get => category; set => category = value; }
        public string Name { get => name; set => this.name = value; }
        public double Relevant { get => relevant; set => relevant = value; }

        public override string ToString()
        {
            string stringRepresentation;
            if (string.IsNullOrWhiteSpace(category))
                stringRepresentation = name;
            else stringRepresentation = category + " = " + name;
            return stringRepresentation;
        }
    }
}
