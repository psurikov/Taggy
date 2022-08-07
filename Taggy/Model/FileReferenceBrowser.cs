using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taggy.Model
{
    public class FileReferenceBrowser
    {
        public static IEnumerable<FileReference> Browse(string location)
        {
            var fileReferences = new List<FileReference>();
            var files = GetFiles(location);
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var tags = TagsConverter.FromString(fileName);
                if (tags != null && tags.Items.Count > 0)
                    fileReferences.Add(new FileReference(file, tags));                
            }

            return fileReferences;
        }

        private static IEnumerable<string> GetFiles(string location)
        {
            var options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            options.RecurseSubdirectories = true;
            var files = Directory.EnumerateFiles(location, "*", options);
            return files;
        }
    }

    public class FileReference
	{
        public FileReference(string filePath, Tags tags)
		{
            this.FilePath = filePath;
            this.Tags = tags;
		}

        public string FilePath { get; set; }
        public Tags Tags { get; set; }
	}
}
