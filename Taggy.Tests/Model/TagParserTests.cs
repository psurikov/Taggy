using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taggy.Model;

namespace Taggy.Tests.Model
{
    [TestClass]
    public class TagParserTests
    {
        [TestMethod]
        public void Parse_None()
        {
            Assert.IsTrue(TagParser.Parse("").Items.Count == 0);
            Assert.IsTrue(TagParser.Parse("name").Items.Count == 0);
            Assert.IsTrue(TagParser.Parse("name ()").Items.Count == 0);
        }

        [TestMethod]
        public void Parse_Items()
        {
            var tags = TagParser.Parse("name @(author=roger, year=1986, biology, mathematics)");
            Assert.IsTrue(tags.GetTagByName("author")?.Value == "roger");
            Assert.IsTrue(tags.GetTagByName("year")?.Value == "1986");
            Assert.IsTrue(tags.GetTagByValue("biology") != null);
            Assert.IsTrue(tags.GetTagByValue("mathematics") != null);
        }
    }
}
