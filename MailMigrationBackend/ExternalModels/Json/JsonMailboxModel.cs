namespace MailMigrationBackend.ExternalModels.Json;

public class JsonMailboxModel
{
    public int MailboxQuota { get; set; }
    
    public double MailboxSize { get; set; }
    
    public string Password { get; set; }
    
    public List<JsonEmailModel> Mails { get; set; }
}