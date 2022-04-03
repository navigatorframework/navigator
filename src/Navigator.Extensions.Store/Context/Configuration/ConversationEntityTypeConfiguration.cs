using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Context.Configuration;

public class ConversationEntityTypeConfiguration : IEntityTypeConfiguration<UniversalConversation>
{
    public void Configure(EntityTypeBuilder<UniversalConversation> builder)
    {
        builder.Property(e => e.FirstInteractionAt)
            .IsRequired();
    }
}