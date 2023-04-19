namespace AuditaScanner.Models.LoginModels;

public class PerfilModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}
