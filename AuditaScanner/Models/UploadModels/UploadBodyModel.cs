namespace AuditaScanner.Models.UploadModels;

public class UploadBodyModel
{
    [JsonProperty("files")]
    public List<FileModel> Files { get; set; }
    [JsonProperty("sender")]
    public SenderModel Sender { get; set; }
}
