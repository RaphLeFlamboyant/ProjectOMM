namespace MailMigrationBackend.ExternalModels.SqLiteProvider;

public class SqLiteEmailModel
{
    public int Id { get; set; }
    
    public int MailboxId { get; set; }
    
    public SqLiteMailboxModel Mailbox { get; set; }
    
    public int FolderId { get; set; }
    
    public SqLiteFolderModel Folder { get; set; }
    
    public string Subject { get; set; }
    
    public string Body { get; set; }
    
    public string From { get; set; }
    
    public string To { get; set; }
    
    public int Size { get; set; }
}