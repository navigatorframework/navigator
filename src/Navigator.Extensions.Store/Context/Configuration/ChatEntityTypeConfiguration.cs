﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entity;

namespace Navigator.Extensions.Store.Context.Configuration
{
    public class ChatEntityTypeConfiguration<TChat> : IEntityTypeConfiguration<TChat> where TChat : Chat
    {
        public virtual void Configure(EntityTypeBuilder<TChat> builder)
        {
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
        }
    }
}