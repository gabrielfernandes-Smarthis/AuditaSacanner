using AuditaScanner.Models;

namespace AuditaScanner.Controllers.LoginControllers;

public interface ILoginController
{
    bool ValidarCpf(string cpf);
    bool ValidarCnpj(string cnpj);
    string GetMd5Hash(string senha);
    Task<TResponse> LoginAsync<TResponse>(string request);
}
