

using Aspose.Words;

namespace AuditaScanner.Controllers.ScannerControllers;

public class PDFController
{
    private readonly string _path;
    private readonly string _name;
    public PDFController(string path, string name)
    {
        _path = path;
        _name = name;
    }

    public void GerarPDF(string imagePath)
    {
        Document doc = new Document();
        DocumentBuilder builder = new DocumentBuilder(doc);
        builder.InsertImage(imagePath);
        string pdfPath = _path + "\\" + _name + ".pdf";
        doc.Save(pdfPath);
    }
}
