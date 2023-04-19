namespace AuditaScanner.Models.PrestadoraModels;

public class PrestadoraModel
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("razaoSocial")]
    public string RazaoSocial { get; set; }
    [JsonProperty("fantasia")]
    public string Fantasia { get; set; }
    [JsonProperty("cnpj")]
    public string Cnpj { get; set; }
    [JsonProperty("tolken")]
    public string Token { get; set; }
}
