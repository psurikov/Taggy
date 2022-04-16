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
                var tagCluster = TagsConverter.FromString(fileName);
                if (tagCluster != null && tagCluster.Items.Count > 0)
                    fileReferences.Add(new FileReference(file, tagCluster));                
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
}
