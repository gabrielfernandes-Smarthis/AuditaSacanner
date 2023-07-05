namespace AuditaScanner.Models.UploadModels;

public class SenderModel
{
    [JsonProperty("cpf")]
    public string Cpf { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("nickname")]
    public string Nickname { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }
}
