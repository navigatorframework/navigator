using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Configuration;

public class ProfileEntityTypeConfiguration : IEntityTypeConfiguration<UniversalProfile>
{
    public void Configure(EntityTypeBuilder<UniversalProfile> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Provider)
            .IsRequired();
        
        builder.Property(e => e.Identification)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();
        
        builder.Property(e => e.LastUpdatedAt)
            .IsRequired();
    }
}