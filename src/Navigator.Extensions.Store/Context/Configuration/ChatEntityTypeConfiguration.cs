using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Context.Configuration;

public class ChatEntityTypeConfiguration : IEntityTypeConfiguration<UniversalChat>
{
    public void Configure(EntityTypeBuilder<UniversalChat> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.Profiles)
            .WithOne();
        
        builder.HasMany(e => e.Users)
            .WithMany(e => e.Chats)
            .UsingEntity<UniversalConversation>(
                e => e.HasOne(e => e.User)
                    .WithMany(e => e.Conversations),
                e=> e.HasOne(e => e.Chat)
                    .WithMany(e => e.Conversations));

        builder.Property(e => e.FirstInteractionAt)
            .IsRequired();
    }
}