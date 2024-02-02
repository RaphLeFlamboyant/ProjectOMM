using System.Text.Json.Serialization;

namespace MailMigrationBackend.ExternalModels.Json;

public class JsonEmailModel
{
    [JsonPropertyName("subject")]
    public string Subject { get; set; }
    
    [JsonPropertyName("body")]
    public string Body { get; set; }
    
    [JsonPropertyName("from")]
    public string From { get; set; }
    
    [JsonPropertyName("to")]
    public string To { get; set; }
    
    [JsonPropertyName("filedInto")]
    public string FiledInto { get; set; }
    
    [JsonPropertyName("size")]
    public int Size { get; set; }
}