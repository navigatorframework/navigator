using System;
using System.Threading.Tasks;
using Navigator.Context;
using Navigator.Entities;
using Navigator.Providers.Telegram.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram;

internal class TelegramNavigatorContextBuilderConversationSource : INavigatorContextBuilderConversationSource
{
    public async Task<Conversation> GetConversationAsync(object? originalEvent)
    {
        return await Task.FromResult((originalEvent as Update ?? throw new InvalidOperationException()).GetConversation());
    }
}