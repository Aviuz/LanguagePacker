using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language_Packer.Model
{
    public class Folder : FileSystemItem
    {
        public List<FileSystemItem> Content = new List<FileSystemItem>();

        public IEnumerable<File> Files
        {
            get
            {
                foreach (var file in Content.Where(i => i is File))
                    yield return file as File;
            }
        }

        public IEnumerable<Folder> Folders
        {
            get
            {
                foreach (var folder in Content.Where(i => i is Folder))
                    yield return folder as Folder;
            }
        }
    }
}
