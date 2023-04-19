using Newtonsoft.Json.Serialization;
using System.Net.Http.Json;

namespace AuditaScanner.Controllers.TipoDocumentoControllers;

public class TipoDocumentoController : ITipoDocumentoController
{
    private readonly HttpClient _httpClient;

    public TipoDocumentoController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TResponse>> GetTiposDocumentos<TResponse>()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(Constants.GetTiposDocumentosUrl);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Erro na resposta HTTP: " + e.Message);
            }

            List<TResponse> tiposDocumentos = JsonConvert.DeserializeObject<List<TResponse>>(response.Content.ReadAsStringAsync().Result);
            return tiposDocumentos;
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao recuperar os tipos de documento " + e.Message);
        }
    }
}
