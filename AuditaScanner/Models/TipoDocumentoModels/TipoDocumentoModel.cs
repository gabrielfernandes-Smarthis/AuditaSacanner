namespace AuditaScanner.Models.TipoDocumentoModels;

public class TipoDocumentoModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Nome { get; set; }
    [JsonProperty("codeDominio")]
    public string CodigoDominio { get; set; }
}
