using MailMigrationBackend.ExternalModels.Json;
using MailMigrationBackend.Models;

namespace MailMigrationBackend.Converters;

public class JsonModelMapper
{
    public MailboxModel MapToInternalModel(JsonFullMailboxModel jsonModel)
    {
        MailboxModel mailboxInternalModel = new()
        {
            EmailAddress = jsonModel.EmailAddress,
            Password = jsonModel.Mailbox.Password
        };

        var folders = new Dictionary<string, FolderModel>();

        foreach (var mail in jsonModel.Mailbox.Mails)
        {
            EmailModel emailInternalModel = new()
            {
                Body = mail.Body,
                Subject = mail.Subject,
                Sender = mail.From,
                Recipients = mail.To,
                Size = mail.Size
            };

            if (!folders.TryGetValue(mail.FiledInto, out var folderInternalModel))
            {
                folderInternalModel = new FolderModel
                {
                    Name = mail.FiledInto
                };
                folders.Add(mail.FiledInto, folderInternalModel);
                mailboxInternalModel.Folders.Add(folderInternalModel);
            }

            emailInternalModel.Folder = folderInternalModel;
            mailboxInternalModel.Emails.Add(emailInternalModel);
        }

        return mailboxInternalModel;
    }

    public JsonFullMailboxModel MapFromInternalModel(MailboxModel internalModel)
    {
        JsonFullMailboxModel fullMailboxJsonModel = new()
        {
            EmailAddress = internalModel.EmailAddress
        };

        JsonMailboxModel mailboxJsonModel = new()
        {
            //
            MailboxQuota = 50,
            MailboxSize = 0
        };

        foreach (var email in internalModel.Emails)
        {
            JsonEmailModel emailJsonModel = new()
            {
                Subject = email.Subject,
                Body = email.Body,
                From = email.Sender,
                To = email.Recipients,
                Size = email.Size,
                FiledInto = email.Folder.Name
            };

            mailboxJsonModel.Mails.Add(emailJsonModel);
            mailboxJsonModel.MailboxSize += ByteScaleConverter.ConvertByteToGigabyte(emailJsonModel.Size);
        }

        fullMailboxJsonModel.Mailbox = mailboxJsonModel;
        return fullMailboxJsonModel;
    }
}