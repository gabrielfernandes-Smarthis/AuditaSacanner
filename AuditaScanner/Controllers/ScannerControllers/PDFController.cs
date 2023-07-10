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
        AdicionarImagem(frentePath, trasPath);
        _doc.Close();
        return pdfPath;
    }

    public void AdicionarImagem(string imagePath)
    {
        ImageData imageData = RedimensionarImagem(imagePath, 1000, 1424);
        Image img = new Image(imageData);
        img.SetHorizontalAlignment(HorizontalAlignment.CENTER);
        img.SetAutoScale(true);
        _doc.Add(img);
    }

    public void AdicionarImagem(string frente, string tras)
    {
        ImageData imageData = RedimensionarImagem(frente, 1000, 1424);
        Image img = new Image(imageData);
        img.SetHorizontalAlignment(HorizontalAlignment.CENTER);
        img.SetAutoScale(true);
        _doc.Add(img);
        _doc.Add(new AreaBreak());
        imageData = RedimensionarImagem(frente, 1000, 1424);
        img = new Image(imageData);
        img.SetHorizontalAlignment(HorizontalAlignment.CENTER);
        img.SetAutoScale(true);
        _doc.Add(img);
    }

    public ImageData RedimensionarImagem(string imagePath, int maxWidth, int maxHeight)
    {
        using (var image = System.Drawing.Image.FromFile(imagePath))
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            var ms = new MemoryStream();
            newImage.Save(ms, image.RawFormat);
            return ImageDataFactory.Create(ms.ToArray());
        }
    }

}
