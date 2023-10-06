using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Context.Configuration;

public class ConversationEntityTypeConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
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