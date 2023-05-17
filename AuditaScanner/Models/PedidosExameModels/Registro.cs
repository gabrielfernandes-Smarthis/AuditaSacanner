namespace AuditaScanner.Models.PedidosExameModels;

public class Registro
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("DataPedido")]
    public string DataPedido { get; set; }

    [JsonProperty("paciente")]
    public Paciente Paciente { get; set; }

    [JsonProperty("responsavel")]
    public Responsavel Responsavel { get; set; }

    [JsonProperty("tipoProcedimento")]
    public TipoProcedimento TipoProcedimento { get; set; }
}
