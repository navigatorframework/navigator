using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Configuration;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store;

public class NavigatorDbContext : NavigatorDbContext<User, Chat>
{
    public NavigatorDbContext(DbContextOptions options) : base(options)
    {
    }
}

public class NavigatorDbContext<TUser, TChat> : DbContext
    where TUser : User
    where TChat : Chat
{
    public DbSet<TUser> Users { get; set; }
    public DbSet<TChat> Chats { get; set; }

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