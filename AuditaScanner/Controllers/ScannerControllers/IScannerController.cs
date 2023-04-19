namespace AuditaScanner.Controllers.ScannerControllers;

public interface IScannerController
{
    ImageFile Scan(DeviceInfo deviceInfo, string formatoImagem);
}