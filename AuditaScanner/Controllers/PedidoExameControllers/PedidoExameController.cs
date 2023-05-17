using System.Net.Http.Json;
using Newtonsoft.Json;

namespace AuditaScanner.Controllers.PedidoExameControllers;

public class PedidoExameController : IPedidoExameController
{
    private readonly HttpClient _httpClient;

    public PedidoExameController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse> GetPedidosExames<TResponse>(int idpedido)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(Constants.GetPedidoExame + idpedido);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Erro na resposta HTTP: " + e.Message);
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(jsonResponse);
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao recuperar prestadoras " + e.Message);
        }
    }
}
