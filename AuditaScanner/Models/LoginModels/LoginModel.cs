namespace AuditaScanner.Models.LoginModels;

public class LoginModel
{
    [JsonProperty("erroCode")]
    public int ErrorCode { get; set; }
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("nickname")]
    public string Nickname { get; set; }

    [JsonProperty("cpf")]
    public string Cpf { get; set; }

    [JsonProperty("login")]
    public string Login { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("isLogado")]
    public bool IsLogado { get; set; }

    [JsonProperty("ultimoAcess")]
    public object UltimoAcess { get; set; }

    [JsonProperty("token")]
    public string Token { get; set; }

    [JsonProperty("avatar")]
    public object Avatar { get; set; }

    [JsonProperty("companies")]
    public List<CompaniesModel> Companies { get; set; }

    [JsonProperty("perfil")]
    public PerfilModel Perfil { get; set; }
}
