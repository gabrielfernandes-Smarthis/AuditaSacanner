using AuditaScanner.Models.PedidosExame;

namespace AuditaScanner.Models.PedidosExameModels;

public class RetornoPedidoExame
{
    [JsonProperty("pedidoExame")]
    public PedidoExame PedidoExame { get; set; }

    [JsonProperty("tiposArquivos")]
    public List<TiposArquivos> TipoArquivo { get; set; }
}
