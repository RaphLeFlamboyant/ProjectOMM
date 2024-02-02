using MailMigrationBackend.ExternalModels.SqLite;
using Microsoft.EntityFrameworkCore;

namespace MailMigrationBackend.IO.Contexts;

public partial class SqLiteDbContext : DbContext
{
    public SqLiteDbContext()
    {
    }

    public SqLiteDbContext(DbContextOptions<SqLiteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<Mail> Mails { get; set; }

    public virtual DbSet<Mailbox> Mailboxes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=sqLiteDb\\SqLiteDb.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Folder>(entity =>
        {
            entity.ToTable("folders");

            entity.Property(e => e.MailboxId).HasColumnName("mailboxId");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Mail>(entity =>
        {
            entity.ToTable("mails");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.MailboxId).HasColumnName("mailboxId");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.To).HasColumnName("to");
        });

        modelBuilder.Entity<Mailbox>(entity =>
        {
            entity.ToTable("mailbox");

            entity.HasIndex(e => e.Email, "IX_mailbox_email").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Quota).HasColumnName("quota");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
