using MailMigrationBackend.ExternalModels.SqLite;
using MailMigrationBackend.Models;

namespace MailMigrationBackend.Mappers;

public class SqLiteModelMapper
{
    public MailboxModel MapToInternalModel(Mailbox mailboxSqLiteModel, List<Folder> folderSqLiteModels, List<Mail> emailSqLiteModels)
    {
        MailboxModel mailboxInternalModel = new()
        {
            EmailAddress = mailboxSqLiteModel.Email,
            Password = mailboxSqLiteModel.Password
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
                Folder = folderIdToInternalModel[emailSqLiteModel.FolderId]
            };
            mailboxInternalModel.Emails.Add(emailInternalModel);
        }

        return mailboxInternalModel;
    }

    public (Mailbox MailboxSqLiteModel,
        List<Folder> FolderSqLiteModels,
        List<Mail> EmailSqLiteModels) MapFromInternalModel(MailboxModel mailboxInternalModel)
    {
        Mailbox mailboxSqLiteModel = new();
        mailboxSqLiteModel.Email = mailboxInternalModel.EmailAddress;
        //
        mailboxSqLiteModel.Quota = 100;

        var folderSqLiteModels = mailboxInternalModel.Folders
            .Select(m => new Folder { Name = m.Name })
            .ToList();

        var emailSqLiteModels = mailboxInternalModel.Emails.Select(m => new Mail
        {
            Subject = m.Subject,
            Body = m.Body,
            From = m.Sender,
            To = m.Recipients,
            Size = m.Size
        }).ToList();

        return (mailboxSqLiteModel, folderSqLiteModels, emailSqLiteModels);
    }
}