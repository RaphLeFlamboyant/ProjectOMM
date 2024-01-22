namespace MailMigrationBackend.ExternalModels.Json;

public class JsonFullMailboxModel
{
    public string EmailAddress { get; set; }
    public JsonMailboxModel Mailbox { get; set; }
}