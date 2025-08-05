using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Entities;
using Navigator.Extensions.Store.Persistence.Configurations;

namespace Navigator.Extensions.Store.Persistence.Context;

public class NavigatorStoreDbContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<User> Users { get; set; }
    public required DbSet<Chat> Chats { get; set; }
    public required DbSet<Conversation> Conversations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ConversationEntityTypeConfiguration());
    }
}