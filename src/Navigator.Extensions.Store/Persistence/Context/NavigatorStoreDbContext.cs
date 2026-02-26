using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Entities;
using Navigator.Extensions.Store.Persistence.Configurations;

namespace Navigator.Extensions.Store.Persistence.Context;

/// <summary>
///     Default <see cref="DbContext"/> for the Navigator store extension.
/// </summary>
/// <param name="options">The database context options.</param>
public class NavigatorStoreDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    ///     Set of persisted users.
    /// </summary>
    public required DbSet<User> Users { get; set; }

    /// <summary>
    ///     Set of persisted chats.
    /// </summary>
    public required DbSet<Chat> Chats { get; set; }

    /// <summary>
    ///     Set of persisted conversations.
    /// </summary>
    public required DbSet<Conversation> Conversations { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ConversationEntityTypeConfiguration());
    }
}
