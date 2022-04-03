using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Entities;
using Navigator.Extensions.Store.Context.Extension;
using Navigator.Extensions.Store.Entities;
using Navigator.Extensions.Store.Mappers;
using Navigator.Extensions.Store.Telegram.Profiles;
using Navigator.Extensions.Store.Telegram.Profiles.Mappers;
using Navigator.Providers.Telegram.Entities;
using Chat = Navigator.Entities.Chat;
using Conversation = Navigator.Entities.Conversation;
using User = Navigator.Entities.User;

namespace Navigator.Extensions.Store.Telegram;


public class NavigatorStoreTelegramExtension : NavigatorStoreModelExtension
{
    public NavigatorStoreTelegramExtension()
    {
        Extension = modelBuilder =>
        {
            modelBuilder.Entity<TelegramUserProfile>(typeBuilder => typeBuilder.HasBaseType<UserProfile>());
            modelBuilder.Entity<TelegramUser>(typeBuilder => typeBuilder.HasBaseType<User>());
            
            modelBuilder.Entity<TelegramChatProfile>(typeBuilder => typeBuilder.HasBaseType<ChatProfile>());
            modelBuilder.Entity<TelegramChat>(typeBuilder => typeBuilder.HasBaseType<Chat>());

            modelBuilder.Entity<TelegramConversationProfile>(typeBuilder => typeBuilder.HasBaseType<ConversationProfile>());
            modelBuilder.Entity<TelegramConversation>(typeBuilder => typeBuilder.HasBaseType<Conversation>());
        };

        ExtensionServices = services =>
        {
            services.AddSingleton<IProviderProfileMapper, TelegramProviderChatProfileMapper>();
            services.AddSingleton<IProviderProfileMapper, TelegramProviderUserProfileMapper>();
            services.AddSingleton<IProviderProfileMapper, TelegramProviderConversationProfileMapper>();
        };
        
        Info = new NavigatorStoreTelegramExtensionInfo(this);
    }

    public override DbContextOptionsExtensionInfo Info { get; }
}