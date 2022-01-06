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

        public override string ToString()
        {
            string stringRepresentation;
            if (string.IsNullOrWhiteSpace(name))
                stringRepresentation = value;
            else stringRepresentation = name + " = " + value;
            return stringRepresentation;
        }

        public static bool operator ==(Tag a, Tag b)
        {
            return a.Name == b.Name && a.Value == b.Value;
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
            return Name.GetHashCode() ^ Value.GetHashCode();
        }

        public bool Equals(Tag other)
        {
            return this == other;
        }
    }
}
