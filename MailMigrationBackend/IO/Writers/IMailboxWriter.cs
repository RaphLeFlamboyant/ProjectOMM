using MailMigrationBackend.Models;

namespace MailMigrationBackend.IO.Writers;

public interface IMailboxWriter
{
    void SaveMailbox(MailboxModel mailboxInternalModel);
}