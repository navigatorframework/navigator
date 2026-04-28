using Navigator.Abstractions.Entities;
using Navigator.Abstractions.Actions.Builder.Extensions;
using Navigator.Actions.Builder.Extensions;
using Navigator.Catalog.Factory;
using Navigator.Abstractions.Catalog.Extensions;
using Telegram.Bot.Types;

namespace Sample;

public static class BotExtensionExamples
{
    public static void RegisterBotExtensionExamples(this BotActionCatalogFactory bot)
    {
        // This action triggers when a user replies directly to a message sent by the bot.
        bot.OnMessage((Bot self, Message message) => self.IsRepliedTo(message))
            .SendText("I see you replied to me!");

        // This action triggers when the bot is mentioned via @username or by its configured first name.
        bot.OnMessage((Bot self, Message message) => self.IsMentioned(message))
            .SendText("Thanks for the mention!");
    }
}
