using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navigator.Extensions.Store.Entities;

namespace Navigator.Extensions.Store.Configuration;

public class NavigatorProviderUserEntity : IEntityTypeConfiguration<INavigatorProviderUserEntity>
{
    public void Configure(EntityTypeBuilder<INavigatorProviderUserEntity> builder)
    {
        builder.HasKey(e => e.Id);
    }
}