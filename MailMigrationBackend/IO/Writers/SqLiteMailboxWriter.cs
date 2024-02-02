using MailMigrationBackend.ExternalModels.SqLite;
using MailMigrationBackend.IO.Contexts;
using MailMigrationBackend.Mappers;
using MailMigrationBackend.Models;

namespace MailMigrationBackend.IO.Writers;

public class SqLiteMailboxWriter : IMailboxWriter
{
    private SqLiteModelMapper _mapper;
    
    public SqLiteMailboxWriter(SqLiteModelMapper mapper)
    {
        _mapper = mapper;
    }
    
    public void SaveMailbox(MailboxModel mailboxInternalModel)
    {
        using var dbContext = new SqLiteDbContext();

        // mapping
        Mailbox mailboxSqLiteModel = new();
        mailboxSqLiteModel.Email = mailboxInternalModel.EmailAddress;
        mailboxSqLiteModel.Password = mailboxInternalModel.Password;
        mailboxSqLiteModel.Quota = 100;

        //save
        dbContext.Mailboxes.Add(mailboxSqLiteModel);
        dbContext.SaveChanges();
        
        // id 
        mailboxSqLiteModel = dbContext.Mailboxes.First(m => m.Email == mailboxSqLiteModel.Email);
        
        // mapping
        var folderSqLiteModels = mailboxInternalModel.Folders
            .Select(m => new Folder { Name = m.Name, MailboxId = mailboxSqLiteModel.Id })
            .ToList();
        
        // save
        dbContext.Folders.AddRange(folderSqLiteModels);
        dbContext.SaveChanges();
        
        // id
        var folderNameToFolder = dbContext.Folders
            .Where(f => f.MailboxId == mailboxSqLiteModel.Id)
            .ToDictionary(f => f.Name);
        
        // mapping
        var emailSqLiteModels = mailboxInternalModel.Emails.Select(m => new Mail
        {
            Subject = m.Subject ?? string.Empty,
            Body = m.Body,
            From = m.Sender,
            To = m.Recipients,
            Size = m.Size,
            FolderId = folderNameToFolder[m.Folder.Name].Id,
            MailboxId = mailboxSqLiteModel.Id
        }).ToList();
        
        // save
        dbContext.Mails.AddRange(emailSqLiteModels);
        dbContext.SaveChanges();
    }
}