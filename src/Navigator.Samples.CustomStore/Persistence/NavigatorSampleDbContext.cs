using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Context;
using Navigator.Extensions.Store.Entity;
using Navigator.Samples.CustomStore.Entity;

namespace Navigator.Samples.CustomStore.Persistence
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