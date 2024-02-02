namespace MailMigrationBackend.ExternalModels.SqLite;

public partial class Folder
{
    public int Id { get; set; }

    public int MailboxId { get; set; }

    public string Name { get; set; } = null!;
}
