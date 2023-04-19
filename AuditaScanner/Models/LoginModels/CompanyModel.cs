namespace AuditaScanner.Models.LoginModels;

public class CompanyModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("razaoSocial")]
    public string RazaoSocial { get; set; }

    [JsonProperty("nickname")]
    public string Nickname { get; set; }

    [JsonProperty("cnpj")]
    public string Cnpj { get; set; }

    [JsonProperty("negocio")]
    public NegocioModel Negocio { get; set; }

    [JsonProperty("appToken")]
    public string AppToken { get; set; }
}
