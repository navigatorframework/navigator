using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Context.Configuration;
using Navigator.Extensions.Store.Entities;

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
    public NavigatorDbContext(DbContextOptions<NavigatorDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Bots.
    /// </summary>
    public required DbSet<Bot> Bots { get; set; }
    
    /// <summary>
    /// Users.
    /// </summary>
    public required DbSet<User> Users { get; set; }
    
    /// <summary>
    /// Chats.
    /// </summary>
    public required DbSet<Chat> Chats { get; set; }
    
    /// <summary>
    /// Conversations.
    /// </summary>
    public required DbSet<Conversation> Conversations { get; set; }
    
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new BotEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ConversationEntityTypeConfiguration());
    }
}