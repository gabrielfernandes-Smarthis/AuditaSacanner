namespace AuditaScanner.Models.PedidosExame;

public class TiposArquivos
{
    [JsonProperty("pedidoExameId")]
    public int PedidoExameId { get; set; }

    [JsonProperty("tipoArquivoId")]
    public int TipoArquivoId { get; set; }

    [JsonProperty("codeDominio")]
    public string CodeDominio { get; set; }

    [JsonProperty("tipoArquivo")]
    public string TipoArquivo { get; set; }

    [JsonProperty("lastNumber")]
    public string LastNumber { get; set; }
}