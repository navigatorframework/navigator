using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entity;

namespace Navigator.Samples.CustomStore.Entity
{
    public class SampleUser : User
    {
        public string Secret { get; set; }
    }

    public class SampleUserEntityTypeConfiguration : IEntityTypeConfiguration<SampleUser>
    {
        public void Configure(EntityTypeBuilder<SampleUser> builder)
        {
            builder.Property(e => e.Secret)
                .HasMaxLength(100);
        }
    }
}