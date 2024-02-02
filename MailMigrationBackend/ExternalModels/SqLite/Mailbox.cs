namespace MailMigrationBackend.ExternalModels.SqLite;

public partial class Mailbox
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Quota { get; set; }
}
