using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Context.Configuration;
using Chat = Navigator.Extensions.Store.Entities.Chat;
using Conversation = Navigator.Extensions.Store.Entities.Conversation;
using User = Navigator.Extensions.Store.Entities.User;

namespace Navigator.Extensions.Store.Context;

/// <summary>
/// Navigator Store Context
/// </summary>
public class NavigatorDbContext : DbContext
{

    /// <inheritdoc />
    protected NavigatorDbContext()
    {
    }

    /// <inheritdoc />
    public NavigatorDbContext(DbContextOptions options) : base(options)
    {
    }

    public required DbSet<User> Users { get; set; }
    public required DbSet<Chat> Chats { get; set; }
    public required DbSet<Conversation> Conversations { get; set; }
    
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ConversationEntityTypeConfiguration());
    }
}