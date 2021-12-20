using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Configuration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UniversalUser>
{
    public void Configure(EntityTypeBuilder<UniversalUser> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.ProviderEntities)
            .WithOne();

        builder.HasMany(e => e.Chats)
            .WithMany(e => e.Users)
            .UsingEntity<UniversalConversation>(
                e => e.HasOne(e => e.Chat)
                    .WithMany(e => e.Conversations),
                e=> e.HasOne(e => e.User)
                    .WithMany(e => e.Conversations));

        builder.Property(e => e.FirstInteractionAt)
            .IsRequired();
    }
}