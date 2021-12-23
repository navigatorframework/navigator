using Microsoft.EntityFrameworkCore.Infrastructure;
using Navigator.Extensions.Store.Context.Extension;

namespace Navigator.Extensions.Store.Telegram;

public class NavigatorStoreTelegramExtensionInfo : NavigatorStoreModelExtensionInfo
{
    public NavigatorStoreTelegramExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
    {
    }

    public override int GetServiceProviderHashCode()
    {
        var hashCode = new HashCode();
        
        hashCode.Add(base.GetServiceProviderHashCode());
        hashCode.Add(Extension);

        return hashCode.ToHashCode();
    }
}