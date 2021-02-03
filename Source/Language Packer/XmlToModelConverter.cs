using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Language_Packer
{
    class XmlToModelConverter
    {
        public static Model.Folder Convert(string xmlPath)
        {
            var entries = new Dictionary<string, List<string>>();
            var documents = new List<Model.File>();

            // Read xml and load all entries
            using (XmlTextReader Reader = new XmlTextReader(xmlPath))
            {
                while (Reader.Read())
                {
                    if (Reader.Name == "entry")
                    {
                        var document = Reader.GetAttribute("document");
                        var name = Reader.GetAttribute("name");
                        var value = Reader.ReadString();
                        if (!string.IsNullOrEmpty(document) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                        {
                            if (!entries.ContainsKey(document))
                                entries.Add(document, new List<string>());
                            entries[document].Add($"<{name}>{value}</{name}>");
                        }
                    }
                }

                Reader.Close();
            }

            // Group entries into documents (files)
            foreach (var documentName in entries.Keys)
            {
                var document = new Model.File();
                document.Name = documentName;
                document.Content = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<LanguageData> ";
                foreach (var entry in entries[documentName])
                    document.Content += "\n\t" + entry;
                document.Content += "\n</LanguageData>";

                documents.Add(document);
            }

            // Ceate tree of files
            var rootFolder = new Model.Folder();
            foreach (var document in documents)
            {
                var currentFolder = rootFolder;

                var pathElements = document.Name.Split('\\');
                for (int i = 0; i < pathElements.Length - 1; i++)
                {
                    var destinationFolder = currentFolder.Folders.FirstOrDefault(f => f.Name == pathElements[i]);
                    if (destinationFolder == null)
                    {
                        destinationFolder = new Model.Folder();
                        destinationFolder.Name = pathElements[i];
                        currentFolder.Content.Add(destinationFolder);
                    }
                    currentFolder = destinationFolder;
                }

                document.Name = pathElements.Last();
                currentFolder.Content.Add(document);
            }

            File.Delete(xmlPath);

            return rootFolder;
        }
    }
}
