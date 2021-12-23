using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Navigator.Extensions.Store.Context.Extension;

public abstract class NavigatorStoreModelExtensionInfo : DbContextOptionsExtensionInfo
{
    public NavigatorStoreModelExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
    {
    }

    public override int GetServiceProviderHashCode()
    {
        
        var hashCode = new HashCode();
        
        return hashCode.ToHashCode();
    }

    public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
    {
        return other is NavigatorStoreModelExtensionInfo;
    }

    public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
    {
    }

    public override bool IsDatabaseProvider { get; }
    public override string LogFragment { get; }
}