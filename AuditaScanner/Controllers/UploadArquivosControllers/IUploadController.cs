using AuditaScanner.Models;
using AuditaScanner.Models.UploadModels;

namespace AuditaScanner.Controllers.UploadArquivosControllers;

public interface IUploadController
{
    Task<TResponse> UploadArquivosAsync<TResponse>(string request);
}
