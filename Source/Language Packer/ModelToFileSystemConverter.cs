using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language_Packer
{
    class ModelToFileSystemConverter
    {
        public static void Convert(string outPath, Model.Folder rootFolder)
        {
            Directory.CreateDirectory(outPath);
            WriteAllFilesInFolder(outPath, rootFolder);
        }

        private static void WriteAllFilesInFolder(string path, Model.Folder folder)
        {
            foreach (var file in folder.Files)
                using (var writer = File.CreateText($"{path}\\{file.Name}"))
                    writer.Write(file.Content);

            foreach(var subFolder in folder.Folders)
            {
                string subFolderPath = $"{path}\\{subFolder.Name}";
                Directory.CreateDirectory(subFolderPath);
                WriteAllFilesInFolder(subFolderPath, subFolder);
            }
        }
    }
}
