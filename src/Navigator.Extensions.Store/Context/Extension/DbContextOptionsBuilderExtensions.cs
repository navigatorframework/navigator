using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Navigator.Extensions.Store.Context.Extension;

public static class DbContextOptionsBuilderExtensions
{
    public static void UsingStoreExtension<TExtension>(this DbContextOptionsBuilder builder) 
        where TExtension : class, IDbContextOptionsExtension, new()
    {
        ((IDbContextOptionsBuilderInfrastructure)builder).AddOrUpdateExtension(new TExtension());
    }
}