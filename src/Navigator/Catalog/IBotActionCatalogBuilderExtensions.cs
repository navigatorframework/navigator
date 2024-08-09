using Navigator.Actions.Builder;
using Navigator.Telegram;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Catalog;

public static class IBotActionCatalogBuilderExtensions
{
    public static IBotActionBuilder OnCommand(this IBotActionCatalogBuilder builder, string command, Delegate handler)
    {
        var actionBuilder = builder.OnUpdate((Update update) => update.Message?.ExtractCommand() == command, handler);

        return actionBuilder;
    }
}