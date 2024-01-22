namespace MailMigrationBackend.ExternalModels.Json;

public class JsonEmailModel
{
    public string Subject { get; set; }
    
    public string Body { get; set; }
    
    public string From { get; set; }
    
    public string To { get; set; }
    
    public string FiledInto { get; set; }
    
    public int Size { get; set; }
}