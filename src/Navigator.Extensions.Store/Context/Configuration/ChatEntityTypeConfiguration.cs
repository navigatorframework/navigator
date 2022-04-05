using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        builder.Property(e => e.Data)
            .HasConversion<string>(
                dictionary => JsonSerializer.Serialize(dictionary, default(JsonSerializerOptions)),
                json => JsonSerializer.Deserialize<Dictionary<string, string>>(json, default(JsonSerializerOptions))
                        ?? new Dictionary<string, string>(),
                ValueComparer.CreateDefault(typeof(IDictionary<string, string>), false));
        
        builder.Property(e => e.FirstInteractionAt)
            .IsRequired();
    }
}