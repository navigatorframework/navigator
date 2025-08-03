using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Persistence.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ExternalId)
            .IsRequired();
        
        builder.HasIndex(e => e.ExternalId)
            .IsUnique();

        builder.HasMany(e => e.Conversations)
            .WithOne(e => e.User);
    }
}