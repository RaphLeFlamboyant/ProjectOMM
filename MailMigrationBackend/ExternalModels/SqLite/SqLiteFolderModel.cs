namespace MailMigrationBackend.ExternalModels.SqLiteProvider;

public class SqLiteFolderModel
{
    public int Id { get; set; }
    
    public int MailboxId { get; set; }
    
    public SqLiteMailboxModel Mailbox { get; set; }
    
    public string Name { get; set; }
}