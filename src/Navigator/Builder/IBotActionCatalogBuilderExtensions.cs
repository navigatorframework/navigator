using Navigator.Telegram;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Builder;

public static class IBotActionCatalogBuilderExtensions
{
    public static IBotActionBuilder OnCommand(this IBotActionCatalogBuilder builder, string command, Delegate handler)
    {
        var actionBuilder = builder.OnUpdate((Update update) => update.Message?.ExtractCommand() == command, handler);

        var id = builder.Actions.FirstOrDefault(pair => pair.Value == actionBuilder.BotAction).Key;

        builder.ActionTypeByAction[id] = $"{typeof(MessageEntityType)}.{nameof(MessageEntityType.BotCommand)}";

        return actionBuilder;
    }
}