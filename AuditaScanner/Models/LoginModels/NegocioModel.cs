namespace AuditaScanner.Models.LoginModels;

public class NegocioModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}
