namespace AuditaScanner.Controllers.PrestadoraControllers;

public interface IPrestadoraController
{
    Task<List<TResponse>> GetPrestadora<TResponse>();
}
