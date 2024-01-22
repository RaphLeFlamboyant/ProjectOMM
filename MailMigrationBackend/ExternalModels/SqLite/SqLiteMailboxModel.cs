namespace MailMigrationBackend.ExternalModels.SqLiteProvider;

public class SqLiteMailboxModel
{
    public int Id { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public int Quota { get; set; }
}