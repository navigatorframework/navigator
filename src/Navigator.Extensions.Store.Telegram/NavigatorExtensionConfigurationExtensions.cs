using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration;
using Navigator.Configuration.Extensions;
using Navigator.Extensions.Store.Extractors;
using Navigator.Extensions.Store.Telegram.Extractors;

namespace Navigator.Extensions.Store.Telegram;

public static class NavigatorExtensionConfigurationExtensions
{
    public static NavigatorConfiguration StoreForTelegram(this NavigatorExtensionConfiguration extensionConfiguration, Action<DbContextOptionsBuilder>? dbContextOptions = default)
    {
        return extensionConfiguration.Extension(configuration =>
        {
            configuration.Services.AddSingleton<IDataExtractor, TelegramChatDataExtractor>();
            configuration.Services.AddSingleton<IDataExtractor, TelegramUserDataExtractor>();
            configuration.Services.AddSingleton<IDataExtractor, TelegramConversationDataExtractor>();
        });
    }
}