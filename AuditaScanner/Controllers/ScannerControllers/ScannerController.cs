using AuditaScanner.Models;

namespace AuditaScanner.Controllers.ScannerControllers;

public class ScannerController : IScannerController
{
    public ScannerController()
    {

    }

    public ImageFile Scan(DeviceInfo deviceInfo, string formatoImagem)
    {
        Scanner novoScan = new Scanner(deviceInfo);
        return novoScan.ScanearDocumento(formatoImagem);
    }
}
