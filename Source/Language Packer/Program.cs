using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language_Packer
{
    /// <summary>
    /// Program includes 3 steps
    ///     1. Converting excel sheet to single xml file
    ///     2. Converting single xml file to class model, and bulding it inside application
    ///     3. Converting bulit model into final file system
    /// </summary>
    class Program
    {
        public const string Help = "Use of program" + "\n" + "LanguagePacker.exe <EXCEL translation file path> <Output path>";

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.Write(Help);
            }

            string sourceFilePath = args[0];
            string destinationRootFolderPath = args[1];

            Convert(sourceFilePath, destinationRootFolderPath);
        }

        static void Convert(string sourceFilePath, string destinationRootFolderPath)
        {
            if (destinationRootFolderPath.EndsWith("\\"))
                destinationRootFolderPath = destinationRootFolderPath.Substring(0, destinationRootFolderPath.Length - 1);

            ExcelToXmlConverter.Convert(sourceFilePath, destinationRootFolderPath);
            var rootFolder = XmlToModelConverter.Convert(destinationRootFolderPath + $"\\{ExcelToXmlConverter.XmlFileName}");
            ModelToFileSystemConverter.Convert(destinationRootFolderPath, rootFolder);
        }
    }
}
