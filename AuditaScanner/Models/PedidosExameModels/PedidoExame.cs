namespace AuditaScanner.Models.PedidosExameModels;

public class PedidoExame
{
    [JsonProperty("codeErro")]
    public int CodeErro { get; set; }

    [JsonProperty("registro")]
    public Registro Registro { get; set; }
}
