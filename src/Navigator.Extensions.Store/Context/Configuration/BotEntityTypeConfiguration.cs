using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Entities;

namespace Navigator.Extensions.Store.Context.Configuration;

internal class BotEntityTypeConfiguration : IEntityTypeConfiguration<Bot>
{
    public void Configure(EntityTypeBuilder<Bot> builder)
    {
        builder.HasBaseType<User>();
    }
}