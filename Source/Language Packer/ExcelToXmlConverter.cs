using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language_Packer
{
    class ExcelToXmlConverter
    {
        public const string XmlFileName = "LanguageData.xml";

        public static void Convert(string excelPath, string outputPath)
        {
            Directory.CreateDirectory(outputPath);
            XML_Exporter.XmlExporter.ExportXMLFromExcel(excelPath, outputPath + $"\\{XmlFileName}");
        }
    }
}
