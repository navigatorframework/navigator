using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Navigator.Entities;
using Navigator.Extensions.Store.Context.Extension;
using Navigator.Extensions.Store.Entities;
using Navigator.Providers.Telegram.Entities;

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
        Info = new NavigatorStoreTelegramExtensionInfo(this);
    }

    public Action<ModelBuilder> Extension { get; }

    public override DbContextOptionsExtensionInfo Info { get; }
}