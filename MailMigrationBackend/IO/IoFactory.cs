using MailMigrationBackend.Converters;
using MailMigrationBackend.Enums;
using MailMigrationBackend.IO.Readers;
using MailMigrationBackend.IO.Writers;
using MailMigrationBackend.Mappers;

namespace MailMigrationBackend.IO;

public class IoFactory
{
    public IMailboxReader GetReader(SourceType sourceType)
    {
        switch (sourceType)
        {
            case SourceType.JSON:
                return new JsonMailboxReader(new JsonModelMapper());
            case SourceType.SQLITE:
                return new SqLiteMailboxReader(new SqLiteModelMapper());
            default:
                throw new NotImplementedException();
        }
    }
    
    public IMailboxWriter GetWriter(SourceType sourceType)
    {
        switch (sourceType)
        {
            case SourceType.JSON:
                return new JsonMailboxWriter(new JsonModelMapper());
            case SourceType.SQLITE:
                return new SqLiteMailboxWriter(new SqLiteModelMapper());
            default:
                throw new NotImplementedException();
        }
    }
}