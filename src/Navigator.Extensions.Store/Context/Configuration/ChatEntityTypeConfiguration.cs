using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Context.Configuration;

public class ChatEntityTypeConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {        
        builder.HasKey(e => e.Id);

        builder.HasMany(e => e.Users)
            .WithMany(e => e.Chats)
            .UsingEntity<Conversation>(
                e => e.HasOne(e => e.User)
                    .WithMany(e => e.Conversations),
                e=> e.HasOne(e => e.Chat)
                    .WithMany(e => e.Conversations));

        builder.Property(e => e.FirstInteractionAt)
            .IsRequired();
    }
}