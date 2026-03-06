using Navigator.Abstractions.Entities;
using Navigator.Actions.Builder.Extensions;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Telegram.Bot.Types;

namespace Sample;

public static class BotCommandPatternExamples
{
    public static void RegisterBotCommandPatternExamples(this BotActionCatalogFactory bot)
    {
        // This action triggers when a user replies directly to a message sent by the bot.
        bot.OnCommand("happy")
            .SendText("I am happy, exact, concrete match!");
        
        bot.OnCommandPattern("^happy.*")
            .SendText("I am happy, partial, pattern match!");
    }
}