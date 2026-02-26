using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Persistence.Configurations;

/// <summary>
///     EF Core entity type configuration for <see cref="Conversation"/>.
/// </summary>
public class ConversationEntityTypeConfiguration : IEntityTypeConfiguration<Conversation>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Chat)
            .WithMany(e => e.Conversations);
        
        builder.HasOne(e => e.User)
            .WithMany(e => e.Conversations);
    }
}
