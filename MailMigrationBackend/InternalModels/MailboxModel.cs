namespace MailMigrationBackend.Models;

public class MailboxModel
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public List<EmailModel> Emails { get; set; } = new List<EmailModel>();
    public List<FolderModel> Folders { get; set; } = new List<FolderModel>();
}