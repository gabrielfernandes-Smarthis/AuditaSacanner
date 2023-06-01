using AuditaScanner.Controllers.UploadArquivosControllers;
using AuditaScanner.Models.UploadModels;
using System.Net.Http.Headers;

namespace AuditaScanner.Services;

public class UploadArquivos
{
    private readonly string AppToken;
    public UploadBodyModel UploadBody { get; set; }

    public UploadArquivos(UploadBodyModel uploadBody, string appToken)
    {
        UploadBody = uploadBody;
        AppToken = appToken;
    }

    public async Task<bool> UploadArquivo()
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppToken);
            //Add the token
            httpClient.DefaultRequestHeaders.Add("token", AppToken);

            UploadController upload = new UploadController(httpClient);

            string UploadBodyJson = JsonConvert.SerializeObject(UploadBody);

            RetornoUploadModel uploadModel = await upload.UploadArquivosAsync<RetornoUploadModel>(UploadBodyJson);

            if (uploadModel.CodeErro != 0)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro ao realizar login" + ex.Message);
            return false;
        }
    }
}
