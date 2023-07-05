namespace AuditaScanner.Models.UploadModels;

public class FileModel
{
    [JsonProperty("extension")]
    public string Extension { get; set; }
    [JsonProperty("file")]
    public string FileHash { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("pedidoExameId")]
    public int PedidoExameId { get; set; }
    [JsonProperty("procedureId")]
    public int? ProcedureId { get; set; }
    [JsonProperty("atendimentoId")]
    public int? AtendimentoId { get; set; }
    [JsonProperty("fileTypeId")]
    public int FileTypeId { get; set; }
    [JsonProperty("paciente")]
    public string Paciente { get; set; }
}
