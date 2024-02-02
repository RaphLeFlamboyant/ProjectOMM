using System.Text.Json.Serialization;

namespace MailMigrationBackend.ExternalModels.Json;

public class JsonMailboxModel
{
    [JsonPropertyName("mailbox_quota")]
    public int MailboxQuota { get; set; }
    
    [JsonPropertyName("mailbox_size")]
    public double MailboxSize { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [JsonPropertyName("mails")]
    public List<JsonEmailModel> Mails { get; set; }
}