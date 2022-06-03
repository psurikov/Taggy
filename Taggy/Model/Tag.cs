using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taggy.Model
{
    /// <summary>
    /// Tag is a descriptive token that can be applied to any electronic document. For example, the book may have an author=Rudyard Kipling
    /// </summary>
    public struct Tag : IEquatable<Tag>
    {
        private string category = "";
        private string value = "";

        public Tag(string value)
        {
            Category = "";
            Value = value;
        }

        public Tag(string name, string value)
        {
            Category = name;
            Value = value;
        }

        public string Category { get => category; set => category = value; }
        public string Value { get => value; set => this.value = value; }

        public override string ToString()
        {
            string stringRepresentation;
            if (string.IsNullOrWhiteSpace(category))
                stringRepresentation = value;
            else stringRepresentation = category + " = " + value;
            return stringRepresentation;
        }

        public static bool operator ==(Tag a, Tag b)
        {
            return a.Category == b.Category && a.Value == b.Value;
        }

        public static bool operator !=(Tag a, Tag b)
        {
            return !(a == b);
        }

        public override bool Equals(object? obj)
        {
            return obj is Tag tag && this == tag;
        }

        public override int GetHashCode()
        {
            return Category.GetHashCode() ^ Value.GetHashCode();
        }

        public bool Equals(Tag other)
        {
            return this == other;
        }
    }
}
