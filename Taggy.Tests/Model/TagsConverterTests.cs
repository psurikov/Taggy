using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taggy.Model;

namespace Taggy.Tests.Model
{
    [TestClass]
    public class TagsConverterTests
    {
        [TestMethod]
        public void ConvertFromString_None()
        {
            Assert.IsTrue(TagsConverter.FromString("").Items.Count == 0);
            Assert.IsTrue(TagsConverter.FromString("name").Items.Count == 0);
            Assert.IsTrue(TagsConverter.FromString("name ()").Items.Count == 0);
        }

        [TestMethod]
        public void ConvertFromString_Items()
        {
            var tags = TagsConverter.FromString("name @(author=roger, year=1986, biology, mathematics)");
            Assert.IsTrue(tags.GetTagByCategory("author")?.Name == "roger");
            Assert.IsTrue(tags.GetTagByCategory("year")?.Name == "1986");
            Assert.IsTrue(tags.GetTagByValue("biology") != null);
            Assert.IsTrue(tags.GetTagByValue("mathematics") != null);
        }

        [TestMethod]
        public void ConvertToString()
        {
            var emptyTags = new Tags();
            var tags = new Tags();
            tags.Items.Add(new Tag("name1", "value1"));
            tags.Items.Add(new Tag("name2", "value2"));
            tags.Items.Add(new Tag("value3"));
            tags.Items.Add(new Tag("value4"));
            Assert.IsTrue(TagsConverter.ToString(emptyTags) == "@()");
            Assert.IsTrue(TagsConverter.ToString(tags) == "@(name1=value1,name2=value2,value3,value4)");
        }
    }
}
