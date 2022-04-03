using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Extensions.Store.Context.Extension;
using Navigator.Extensions.Store.Extractors;
using Navigator.Extensions.Store.Telegram.Extractors;

namespace Navigator.Extensions.Store.Telegram;


public class NavigatorStoreTelegramExtension : NavigatorStoreModelExtension
{
    public NavigatorStoreTelegramExtension()
    {
        ExtensionServices = services =>
        {
            services.AddSingleton<IDataExtractor, TelegramChatDataExtractor>();
            services.AddSingleton<IDataExtractor, TelegramUserDataExtractor>();
            services.AddSingleton<IDataExtractor, TelegramConversationDataExtractor>();
        };
        
        Info = new NavigatorStoreTelegramExtensionInfo(this);
    }

    public override DbContextOptionsExtensionInfo Info { get; }
}