namespace AuditaScanner.Controllers.TipoDocumentoControllers;

public interface ITipoDocumentoController
{
    Task<List<TResponse>> GetTiposDocumentos<TResponse>();
}
