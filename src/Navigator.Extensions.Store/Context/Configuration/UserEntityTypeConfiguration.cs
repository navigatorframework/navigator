﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entity;

namespace Navigator.Extensions.Store.Context.Configuration
{
    public class UserEntityTypeConfiguration<TUser> : IEntityTypeConfiguration<TUser> where TUser : User
    {
        public virtual void Configure(EntityTypeBuilder<TUser> builder)
        {
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
        }
    }
}