namespace AuditaScanner.Models.LoginModels;

public class CompaniesModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("fantasia")]
    public string Fantasia { get; set; }

    [JsonProperty("razaoSocial")]
    public string RazaoSocial { get; set; }

    [JsonProperty("cnpj")]
    public string Cnpj { get; set; }

    [JsonProperty("appToken")]
    public string AppToken { get; set; }


    public override string ToString()
    {
        return Fantasia;
    }
}
