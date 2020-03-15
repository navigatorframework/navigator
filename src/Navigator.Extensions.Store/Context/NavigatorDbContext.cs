using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Context.Configuration;
using Navigator.Extensions.Store.Entity;

namespace Navigator.Extensions.Store.Context
{
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
        public DbSet<Conversation> Conversations { get; set; }

        protected NavigatorDbContext()
        {
        }

        public NavigatorDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ConversationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration<TChat>());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration<TUser>());

        }
    }
}