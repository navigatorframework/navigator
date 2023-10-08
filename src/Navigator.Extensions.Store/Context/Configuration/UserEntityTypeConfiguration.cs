using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Context.Configuration;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Username);
        builder.Property(e => e.FirstName);
        builder.Property(e => e.LastName);
        builder.Property(e => e.LanguageCode);
        builder.Property(e => e.IsPremium);
        builder.Property(e => e.HasBotInAttachmentMenu);

        builder.Property(e => e.FirstInteractionAt)
            .IsRequired();

        builder.HasMany(e => e.Chats)
            .WithMany(e => e.Users)
            .UsingEntity<Conversation>(
                e => e.HasOne(e => e.Chat)
                    .WithMany(e => e.Conversations),
                e => e.HasOne(e => e.User)
                    .WithMany(e => e.Conversations));
    }
}