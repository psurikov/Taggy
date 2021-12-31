using System.Linq;
using System.Text.RegularExpressions;

namespace Taggy.Model
{
    public class TagParser
    {
        public static TagCluster Parse(string fileName)
        {
            var regex = "\\((?<tags>.*)\\)";
            var match = Regex.Match(fileName, regex);
            if (match.Success)
            {
                var tags = new TagCluster();
                var tagsGroup = match.Groups["tags"].Value;
                var tagsPairList = tagsGroup.Split(',').Where(p => !string.IsNullOrEmpty(p));
                foreach (var tagsPair in tagsPairList)
                {
                    var nameValue = tagsPair.Split('=').Where(p => !string.IsNullOrEmpty(p)).ToList();
                    if (nameValue.Count > 1)
                    {
                        var name = nameValue[0].Trim();
                        var value = nameValue[1].Trim();
                        
                        // handling special case for "Tags"
                        if (name.ToLower() == "tags")
                        {
                            var innerValues = value.Split(',').Where(v => !string.IsNullOrEmpty(v)).ToList();
                            foreach (var innerValue in innerValues)
                            {
                                var tag = new Tag(name, innerValue.Trim());
                                tags.Items.Add(tag);
                            }
                        }
                        else
                        {
                            var tag = new Tag(name, value);
                            tags.Items.Add(tag);
                        }
                    }
                    if (nameValue.Count > 0)
                    {
                        var value = nameValue[0];
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
