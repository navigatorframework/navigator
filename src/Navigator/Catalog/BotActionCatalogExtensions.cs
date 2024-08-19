using Navigator.Actions.Builder;
using Navigator.Telegram;
using Telegram.Bot.Types;

namespace Navigator.Catalog;

public static class BotActionCatalogExtensions
{
    public static IBotActionBuilder OnCommand(this IBotActionCatalogFactory factory, string command, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate((Update update) => update.Message?.ExtractCommand() == command, handler);

        return actionBuilder;
    }
}