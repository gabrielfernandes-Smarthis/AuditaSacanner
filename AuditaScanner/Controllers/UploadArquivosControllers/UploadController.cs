using System.Net.Http.Json;

namespace AuditaScanner.Controllers.UploadArquivosControllers;

public class UploadController : IUploadController
{
    private readonly HttpClient _httpClient;

    public UploadController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse> UploadArquivosAsync<TResponse>(string request)
    {
        try
        {
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(Constants.UploadArquivosUrl, content);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Erro na resposta HTTP: " + e.Message);
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            await Console.Out.WriteLineAsync(jsonResponse);
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao se comunicar com endpoint upload: \n" + e.Message);
        }
    }
}
