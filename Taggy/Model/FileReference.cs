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
    internal class FileReference
    {
        private string filePath = "";
        private TagCluster tagCluster = new TagCluster();

        public FileReference(string filePath, TagCluster tags)
        {
            FilePath = filePath;
            TagCluster = tags;
        }

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public TagCluster TagCluster 
        {
            get { return tagCluster; }
            set { tagCluster = value; }
        }
    }
}
