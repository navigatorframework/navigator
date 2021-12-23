using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Navigator.Extensions.Store.Context.Extension;

public abstract class NavigatorStoreModelExtension : IDbContextOptionsExtension
{
    public Action<ModelBuilder> Extension { get; }
    public void ApplyServices(IServiceCollection services)
    {
        
    }

    public void Validate(IDbContextOptions options)
    {
        
    }

    public abstract DbContextOptionsExtensionInfo Info { get; }
}