using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Persistence.Configurations;

public class ChatEntityTypeConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.ExternalId)
            .IsRequired();
        
        builder.HasIndex(e => e.ExternalId)
            .IsUnique();
        
        builder.HasMany(e => e.Conversations)
            .WithOne(e => e.Chat);
    }
}