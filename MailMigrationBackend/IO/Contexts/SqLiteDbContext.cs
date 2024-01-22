using MailMigrationBackend.ExternalModels.SqLiteProvider;
using Microsoft.EntityFrameworkCore;

namespace MailMigrationBackend.IO.Contexts;

public class SqLiteDbContext : DbContext
{
    private const string DbFile = "SqLiteDb.db";
    
    public DbSet<SqLiteMailboxModel> Mailbox { get; set; }
    public DbSet<SqLiteFolderModel> Folders { get; set; }
    public DbSet<SqLiteEmailModel> Emails { get; set; }
    
    public string DbPath { get; }
    
    public SqLiteDbContext()
    {
        DbPath = Path.GetFullPath(DbFile);
    }
    
    //
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}