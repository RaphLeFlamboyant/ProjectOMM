using MailMigrationBackend.Converters;
using MailMigrationBackend.ExternalModels.SqLiteProvider;
using MailMigrationBackend.IO.Contexts;
using MailMigrationBackend.Models;

namespace MailMigrationBackend.IO.Writers;

public class SqLiteMailboxWriter
{
    private SqLiteModelMapper _mapper;
    
    public SqLiteMailboxWriter(SqLiteModelMapper mapper)
    {
        _mapper = mapper;
    }
    
    public void SaveMailbox(MailboxModel mailboxInternalModel)
    {
        using var dbContext = new SqLiteDbContext();

        var mappedItems = _mapper.MapFromInternalModel(mailboxInternalModel);
        
        dbContext.Mailbox.Add(mappedItems.MailboxSqLiteModel);
        dbContext.Folders.AddRange(mappedItems.FolderSqLiteModels);
        dbContext.Emails.AddRange(mappedItems.EmailSqLiteModels);
        dbContext.SaveChanges();
    }
}