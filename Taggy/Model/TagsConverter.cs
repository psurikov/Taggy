using System.Linq;
using System.Text.RegularExpressions;

namespace Taggy.Model
{
    public class TagsConverter
    {
        public static Tags FromString(string tagsString)
        {
            var regex = "@\\((?<tags>.*)\\)";
            var match = Regex.Match(tagsString, regex);
            if (match.Success)
            {
                var tags = new Tags();
                var tagsGroup = match.Groups["tags"].Value;
                var tagsPairList = tagsGroup.Split(',').Where(p => !string.IsNullOrEmpty(p));
                foreach (var tagsPair in tagsPairList)
                {
                    var index = tagsPair.IndexOf('=');
                    if (index >= 0)
                    {
                        var name = tagsPair.Substring(0, index).Trim();
                        var value = tagsPair.Substring(index + 1).Trim();
                        var tag = TagCache.GetTag(name, value);
                        tags.Items.Add(tag);
                    }
                    else
                    {
                        var value = tagsPair.Trim();
                        var tag = TagCache.GetTag(value);
                        tags.Items.Add(tag);
                    }
                }

                return tags;
            }
            else
                return new Tags();
        }

        public static string ToString(Tags tags)
        {
            static string TagToString(Tag tag)
            {
                if (!string.IsNullOrWhiteSpace(tag.Category))
                    return tag.Category + "=" + tag.Name;
                return tag.Name;
            }

            return string.Format("@({0})", string.Join(",", tags.Items.Select(TagToString)));
        }
    }
}
