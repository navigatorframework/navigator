using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Context.Configuration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.Chats)
            .WithMany(e => e.Users)
            .UsingEntity<Conversation>(
                e => e.HasOne(e => e.Chat)
                    .WithMany(e => e.Conversations),
                e=> e.HasOne(e => e.User)
                    .WithMany(e => e.Conversations));

        builder.Property(e => e.FirstInteractionAt)
            .IsRequired();
    }
}