using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Context.Configuration;

internal class ChatEntityTypeConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title);
        builder.Property(e => e.Type);
        builder.Property(e => e.IsForum);

        builder.Property(e => e.FirstInteractionAt)
            .IsRequired();

        builder.HasMany(e => e.Users)
            .WithMany(e => e.Chats)
            .UsingEntity<Conversation>(
                r => r.HasOne(e => e.User)
                    .WithMany(e => e.Conversations).HasForeignKey(e=> e.UserId),
                l => l.HasOne(e => e.Chat)
                    .WithMany(e => e.Conversations).HasForeignKey(e=> e.ChatId));
        // builder.HasMany(e => e.Users)
        //     .WithMany(e => e.Chats)
        //     .UsingEntity<Conversation>();
    }
}