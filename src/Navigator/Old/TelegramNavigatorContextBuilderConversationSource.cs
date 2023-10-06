using Navigator.Context.Builder;
using Navigator.Entities;
using Telegram.Bot.Types;

namespace Navigator.Old;

internal class TelegramNavigatorContextBuilderConversationSource : INavigatorContextBuilderConversationSource
{
    public async Task<Conversation> GetConversationAsync(object? originalEvent)
    {
        return await Task.FromResult((originalEvent as Update ?? throw new InvalidOperationException()).GetConversation());
    }
}