namespace MailMigrationBackend.Models;

public class EmailModel
{
    public string Subject { get; set; }
    
    public string Body { get; set; }
    
    public string Sender { get; set; }
    
    public string Recipients { get; set; }
    
    public FolderModel Folder { get; set; }
    
    public int Size { get; set; }
}