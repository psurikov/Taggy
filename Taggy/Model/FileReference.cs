using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taggy.Model
{
    /// <summary>
    /// File reference that includes various descriptive attributes.
    /// </summary>
    public class FileReference
    {
        private string filePath = "";
        private Tags tagCluster = new Tags();

        public FileReference(string filePath, Tags tags)
        {
            FilePath = filePath;
            TagCluster = tags;
        }

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public string FilePathUntagged
        {
            get
            {
                var atIndex = filePath.IndexOf('@');
                if (atIndex >= 0)
                {
                    var filePathUntagged = filePath.Substring(0, atIndex).TrimEnd();
                    return filePathUntagged;
                }

                return filePath;
            }
        }

        public Tags TagCluster 
        {
            get { return tagCluster; }
            set { tagCluster = value; }
        }
    }
}
