using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Abstractions.Entity;

namespace Navigator.Extensions.Store.Context.Configuration
{
    /// <inheritdoc />
    public class ConversationEntityTypeConfiguration : IEntityTypeConfiguration<Conversation>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(e => new {e.ChatId, e.UserId});

            builder.HasOne(e => e.User)
                .WithMany(e => e.Conversations)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Chat)
                .WithMany(e => e.Conversations)
                .HasForeignKey(e => e.ChatId);
        }
    }
}