using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Abstractions.Entity;
using Navigator.Extensions.Store.Context;
using Navigator.Samples.Echo.Entity;

namespace Navigator.Samples.Echo.Persistence
{
    public class NavigatorSampleDbContext : NavigatorDbContext<SampleUser, Chat>
    {
        public NavigatorSampleDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new SampleUserEntityTypeConfiguration());
        }
    }
}