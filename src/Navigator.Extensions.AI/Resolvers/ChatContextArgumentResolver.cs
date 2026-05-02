using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Extensions.AI.Models;
using Navigator.Extensions.AI.Services;
using Telegram.Bot.Types;

namespace Navigator.Extensions.AI.Resolvers;

/// <summary>
///     Resolves <see cref="ChatContext" /> arguments for bot actions.
/// </summary>
public class ChatContextArgumentResolver : IArgumentResolver
{
    private readonly IChatContextStore _chatContextStore;

    /// <summary>
    ///     Initializes a new resolver instance.
    /// </summary>
    /// <param name="chatContextStore">The store used to load chat context data.</param>
    public ChatContextArgumentResolver(IChatContextStore chatContextStore)
    {
        _chatContextStore = chatContextStore;
    }

    /// <summary>
    ///     Resolves a chat context argument for the current update when requested.
    /// </summary>
    /// <param name="type">The parameter type to resolve.</param>
    /// <param name="update">The current Telegram update.</param>
    /// <param name="action">The resolved bot action.</param>
    /// <returns>The chat context when requested; otherwise, <see langword="null" />.</returns>
    public async ValueTask<object?> GetArgument(Type type, Update update, BotAction action)
    {
        return type == typeof(ChatContext)
            ? await _chatContextStore.GetForUpdateAsync(update)
            : null;
    }
}
