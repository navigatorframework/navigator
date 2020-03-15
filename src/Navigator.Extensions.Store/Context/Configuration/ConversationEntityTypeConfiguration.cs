﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entity;

namespace Navigator.Extensions.Store.Context.Configuration
{
    public class ConversationEntityTypeConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(e => new {e.ChatId, e.UserId});

            builder.HasOne(e => e.User)
                .WithMany(e => e.Conversations)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Chat)
                .WithMany(e => e.Conversations)
                .HasForeignKey(e => e.ChatId);
            
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
        }
    }
}