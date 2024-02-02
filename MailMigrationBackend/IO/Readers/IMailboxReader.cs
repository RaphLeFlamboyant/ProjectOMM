using MailMigrationBackend.Models;

namespace MailMigrationBackend.IO.Readers;

public interface IMailboxReader
{
    MailboxModel ReadMailbox(string emailAddress);
}