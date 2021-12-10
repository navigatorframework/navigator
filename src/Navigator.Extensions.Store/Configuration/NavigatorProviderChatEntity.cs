using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Navigator.Extensions.Store.Configuration;

public class NavigatorProviderChatEntity : IEntityTypeConfiguration<INavigatorProviderChatEntity>
{
    public void Configure(EntityTypeBuilder<INavigatorProviderChatEntity> builder)
    {
        builder.HasKey(e => e.Id);
    }
}