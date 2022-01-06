using System.Linq;
using System.Text.RegularExpressions;

namespace Taggy.Model
{
    public class TagParser
    {
        public static TagCluster Parse(string fileName)
        {
            var regex = "@\\((?<tags>.*)\\)";
            var match = Regex.Match(fileName, regex);
            if (match.Success)
            {
                var tags = new TagCluster();
                var tagsGroup = match.Groups["tags"].Value;
                var tagsPairList = tagsGroup.Split(',').Where(p => !string.IsNullOrEmpty(p));
                foreach (var tagsPair in tagsPairList)
                {
                    var index = tagsPair.IndexOf('=');
                    if (index >= 0)
                    {
                        var name = tagsPair.Substring(0, index).Trim();
                        var value = tagsPair.Substring(index + 1).Trim();
                        var tag = new Tag(name, value);
                        tags.Items.Add(tag);
                    }
                    else
                    {
                        var value = tagsPair.Trim();
                        var tag = new Tag(value);
                        tags.Items.Add(tag);
                    }
                }

                return tags;
            }
            else
                return new TagCluster();
        }
    }
}
