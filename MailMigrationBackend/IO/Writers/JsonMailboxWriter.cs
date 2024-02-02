using System.Text.Json;
using MailMigrationBackend.Converters;
using MailMigrationBackend.Models;

namespace MailMigrationBackend.IO.Writers;

public class JsonMailboxWriter : IMailboxWriter
{
    private JsonModelMapper _mapper;

    public JsonMailboxWriter(JsonModelMapper mapper)
    {
        _mapper = mapper;
    }

    public void SaveMailbox(MailboxModel mailboxInternalModel)
    {
        var jsonModel = _mapper.MapFromInternalModel(mailboxInternalModel);
        var jsonContent = JsonSerializer.Serialize(jsonModel.Mailbox);
        
        File.WriteAllText($"jsonDb\\{mailboxInternalModel.EmailAddress}.json", jsonContent);
    }
}