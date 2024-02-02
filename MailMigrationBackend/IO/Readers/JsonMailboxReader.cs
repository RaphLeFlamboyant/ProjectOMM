using System.Text.Json;
using MailMigrationBackend.Converters;
using MailMigrationBackend.ExternalModels.Json;
using MailMigrationBackend.Models;

namespace MailMigrationBackend.IO.Readers;

public class JsonMailboxReader : IMailboxReader
{
    private JsonModelMapper _mapper;

    public JsonMailboxReader(JsonModelMapper mapper)
    {
        _mapper = mapper;
    }

    public MailboxModel ReadMailbox(string emailAddress)
    {
        var jsonText = File.ReadAllText($"jsonDb\\{emailAddress}.json");
        var jsonModel = new JsonFullMailboxModel
        {
            EmailAddress = emailAddress,
            Mailbox = JsonSerializer.Deserialize<JsonMailboxModel>(jsonText)
        };
            
        return _mapper.MapToInternalModel(jsonModel);
    }
}