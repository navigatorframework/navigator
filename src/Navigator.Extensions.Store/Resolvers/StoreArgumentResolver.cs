using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Abstractions.Telegram;
using Navigator.Extensions.Store.Entities;
using Navigator.Extensions.Store.Persistence.Context;
using Navigator.Extensions.Store.Services;
using Telegram.Bot.Types;
using Chat = Navigator.Extensions.Store.Entities.Chat;
using User = Navigator.Extensions.Store.Entities.User;

namespace Navigator.Extensions.Store.Resolvers;

/// <summary>
///     Resolves store entities (<see cref="User"/>, <see cref="Chat"/>, <see cref="Conversation"/>)
///     as action method arguments from the current Telegram update.
/// </summary>
/// <typeparam name="TDbContext">The database context type.</typeparam>
public class StoreArgumentResolver<TDbContext> : IArgumentResolver where TDbContext : NavigatorStoreDbContext
{
    private readonly INavigatorStore<TDbContext> _store;

    /// <summary>
    ///     Creates a new <see cref="StoreArgumentResolver{TDbContext}"/>.
    /// </summary>
    /// <param name="store">The Navigator store service.</param>
    public StoreArgumentResolver(INavigatorStore<TDbContext> store)
    {
        _store = store;
    }

    /// <inheritdoc />
    public async ValueTask<object?> GetArgument(Type type, Update update, BotAction action)
    {
        return type switch
        {
            not null when type == typeof(User) && update.GetUserOrDefault() is { } telegramUser
                => await _store.GetUserAsync(telegramUser.Id),
            not null when type == typeof(Chat) && update.GetChatOrDefault() is { } telegramChat
                => await _store.GetChatAsync(telegramChat.Id),
            not null when type == typeof(Conversation)
                => GetConversation(update),
            _ => null
        };

        async Task<Conversation?> GetConversation(Update telegramUpdate)
        {
            var telegramUser = telegramUpdate.GetUserOrDefault();
            
            if (telegramUser == null) return null;
            
            var telegramChat = telegramUpdate.GetChatOrDefault();
            
            return await _store.GetConversationAsync(telegramUser.Id, telegramChat?.Id);
        }
    }
}
