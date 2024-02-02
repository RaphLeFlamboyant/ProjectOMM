using MailMigrationBackend.IO.Contexts;
using MailMigrationBackend.Mappers;
using MailMigrationBackend.Models;

namespace MailMigrationBackend.IO.Readers;

public class SqLiteMailboxReader : IMailboxReader
{
    private SqLiteModelMapper _mapper;

    public SqLiteMailboxReader(SqLiteModelMapper mapper)
    {
        _mapper = mapper;
    }

    public MailboxModel ReadMailbox(string emailAddress)
    {
        using var dbContext = new SqLiteDbContext();

        var sqLiteMailbox = dbContext.Mailboxes.First(m => m.Email == emailAddress);
        var sqLiteFolders = dbContext.Folders.Where(f => f.MailboxId == sqLiteMailbox.Id).ToList();
        var sqLiteMails = dbContext.Mails.Where(m => m.MailboxId == sqLiteMailbox.Id).ToList();

        return _mapper.MapToInternalModel(sqLiteMailbox, sqLiteFolders, sqLiteMails);
    }
}