using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Image = iText.Layout.Element.Image;
using HorizontalAlignment = iText.Layout.Properties.HorizontalAlignment;

namespace AuditaScanner.Controllers.ScannerControllers;

public class PdfController
{
    private readonly string _path;
    private readonly string _name;
    private Document _doc;
    private PdfWriter _pdfWriter;

    public PdfController(string path, string name)
    {
        _path = path;
        _name = name;
    }

    public string GerarPDF(string imagePath)
    {
        string pdfPath = _path + "\\" + _name + ".pdf";
        _pdfWriter = new PdfWriter(pdfPath);
        _doc = new Document(new PdfDocument(_pdfWriter));
        AdicionarImagem(imagePath);
        _doc.Close();
        return pdfPath;
    }

    public string GerarPDF(string frentePath, string trasPath)
    {
        string pdfPath = _path + "\\" + _name + ".pdf";
        _pdfWriter = new PdfWriter(pdfPath);
        _doc = new Document(new PdfDocument(_pdfWriter));
        AdicionarImagem(frentePath);
        AdicionarImagem(trasPath);
        _doc.Close();
        return pdfPath;
    }

    public void AdicionarImagem(string imagePath)
    {
        ImageData imageData = ImageDataFactory.Create(imagePath);
        Image img = new Image(imageData);
        img.SetHorizontalAlignment(HorizontalAlignment.CENTER);
        img.SetAutoScale(true);
        _doc.Add(img);
        _doc.Add(new AreaBreak());
    }
}
