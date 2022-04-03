using Microsoft.EntityFrameworkCore;
using Navigator.Entities;
using Navigator.Extensions.Store.Context.Configuration;
using Navigator.Extensions.Store.Context.Extension;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Context;

public class NavigatorDbContext : DbContext
{
    public DbSet<UniversalUser> Users { get; set; }
    public DbSet<UniversalChat> Chats { get; set; }
    public DbSet<UniversalConversation> Conversations { get; set; }

    protected NavigatorDbContext()
    {
    }

    private readonly IList<Action<ModelBuilder>> _entityTypeConfigurations = new List<Action<ModelBuilder>>();

    public NavigatorDbContext(DbContextOptions options) : base(options)
    {
        foreach (var extension in options.Extensions
                     .OfType<NavigatorStoreModelExtension>()
                     .Select(e => e.Extension))
        {
            if (extension is not null) _entityTypeConfigurations.Add(extension);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ConversationEntityTypeConfiguration());

        modelBuilder.Entity<Chat>(b =>
        {
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<User>(b =>
        {
            b.HasKey(e => e.Id);
        });
        modelBuilder.Entity<Conversation>(b =>
        {
            b.HasKey(e => e.Id);
        });

        foreach (var entityTypeConfiguration in _entityTypeConfigurations)
        {
            entityTypeConfiguration.Invoke(modelBuilder);
        }
    }
}