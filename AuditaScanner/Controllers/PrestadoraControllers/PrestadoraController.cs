using System.Net.Http.Json;

namespace AuditaScanner.Controllers.PrestadoraControllers;

public class PrestadoraController : IPrestadoraController
{
    private readonly HttpClient _httpClient;

    public PrestadoraController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TResponse>> GetPrestadora<TResponse>()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(Constants.GetPrestadorasUrl);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Erro na resposta HTTP: " + e.Message);
            }

            return await response.Content.ReadFromJsonAsync<List<TResponse>>();
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao recuperar prestadoras " + e.Message);
        }
    }
}
