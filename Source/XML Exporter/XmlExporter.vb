Public Module XmlExporter
    Public Sub ExportXMLFromExcel(inputPath As String, outputPath As String)
        Dim objFSO = CreateObject("Scripting.FileSystemObject")

        Dim src_file = objFSO.GetAbsolutePathName(inputPath)
        Dim dest_file = objFSO.GetAbsolutePathName(outputPath)

        Dim oExcel = CreateObject("Excel.Application")

        Dim oBook = oExcel.Workbooks.Open(src_file)

        Dim map = oBook.XmlMaps("LanguageData")

        oBook.SaveAsXMLDATA(dest_file, map)

        oBook.Close(False)
        oExcel.Quit
    End Sub
End Module
