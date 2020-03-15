using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Context.Configuration;
using Navigator.Extensions.Store.Entity;

namespace Navigator.Extensions.Store.Context
{
    public class NavigatorDbContext : NavigatorDbContext<User, Chat>
    {
    }

    public class NavigatorDbContext<TUser, TChat> : DbContext
        where TUser : User
        where TChat : Chat
    {
        public DbSet<TUser> Users;
        public DbSet<TChat> Chats;
        public DbSet<Conversation> Conversations;

        protected NavigatorDbContext()
        {
        }

        public NavigatorDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConversationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration<TChat>());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration<TUser>());

            base.OnModelCreating(modelBuilder);
        }
    }
}