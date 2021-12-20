using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Configuration;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store;

public class NavigatorDbContext : DbContext
{
    public DbSet<UniversalUser> Users { get; set; }
    public DbSet<UniversalChat> Chats { get; set; }

    protected DbSet<INavigatorProviderChatEntity> NavigatorProviderUserEntities { get; set; }
    protected DbSet<INavigatorProviderChatEntity> NavigatorProviderChatEntities { get; set; }

    protected NavigatorDbContext()
    {
    }

    public NavigatorDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ConversationEntityTypeConfiguration());
    }
}