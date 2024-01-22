using MailMigrationBackend.ExternalModels.SqLiteProvider;
using MailMigrationBackend.Models;

namespace MailMigrationBackend.Converters;

public class SqLiteModelMapper
{
    public MailboxModel MapToInternalModel(SqLiteMailboxModel MailboxSqLiteModel, List<SqLiteFolderModel> folderSqLiteModels, List<SqLiteEmailModel> emailSqLiteModels)
    {
        MailboxModel mailboxInternalModel = new()
        {
            EmailAddress = MailboxSqLiteModel.Email,
            Password = MailboxSqLiteModel.Password
        };

        var folderIdToInternalModel = new Dictionary<int, FolderModel>();

        foreach (var folderSqLiteModel in folderSqLiteModels)
        {
            FolderModel folderInternalModel = new()
            {
                Name = folderSqLiteModel.Name
            };
            
            mailboxInternalModel.Folders.Add(folderInternalModel);
            folderIdToInternalModel.Add(folderSqLiteModel.Id, folderInternalModel);
        }

        foreach (var emailSqLiteModel in emailSqLiteModels)
        {
            EmailModel emailInternalModel = new()
            {
                Subject = emailSqLiteModel.Subject,
                Body = emailSqLiteModel.Body,
                Sender = emailSqLiteModel.From,
                Recipients = emailSqLiteModel.To,
                Size = emailSqLiteModel.Size,
                //
                Folder = folderIdToInternalModel[emailSqLiteModel.Id]
            };
            mailboxInternalModel.Emails.Add(emailInternalModel);
        }

        return mailboxInternalModel;
    }

    public (SqLiteMailboxModel MailboxSqLiteModel,
        List<SqLiteFolderModel> FolderSqLiteModels,
        List<SqLiteEmailModel> EmailSqLiteModels) MapFromInternalModel(MailboxModel mailboxInternalModel)
    {
        SqLiteMailboxModel mailboxSqLiteModel = new();
        mailboxSqLiteModel.Email = mailboxInternalModel.EmailAddress;
        //
        mailboxSqLiteModel.Quota = 100;

        var folderSqLiteModels = mailboxInternalModel.Folders
            .Select(m => new SqLiteFolderModel { Name = m.Name, Mailbox = mailboxSqLiteModel })
            .ToList();

        var folderNameToFolder = folderSqLiteModels.ToDictionary(f => f.Name);

        var emailSqLiteModels = mailboxInternalModel.Emails.Select(m => new SqLiteEmailModel
        {
            Subject = m.Subject,
            Body = m.Body,
            From = m.Sender,
            To = m.Recipients,
            Size = m.Size,
            //
            Folder = folderNameToFolder[m.Folder.Name]
        }).ToList();

        return (mailboxSqLiteModel, folderSqLiteModels, emailSqLiteModels);
    }
}