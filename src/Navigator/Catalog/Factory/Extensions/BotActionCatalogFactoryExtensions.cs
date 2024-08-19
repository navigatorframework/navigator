using Navigator.Actions;
using Navigator.Actions.Builder;
using Navigator.Client;
using Navigator.Telegram;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Catalog.Factory.Extensions;

public static class BotActionCatalogFactoryExtensions
{
    // public static IBotActionBuilder OnUpdate(this IBotActionCatalogFactory factory, Func<Update, Task<bool>> condition, Delegate handler)
    // {
    //     var actionBuilder = factory.OnUpdate(condition, handler);
    //
    //     return actionBuilder;
    // }
    //
    // public static IBotActionBuilder OnUpdate(this IBotActionCatalogFactory factory, Func<Update, Task<bool>> condition,
    //     Func<INavigatorClient, Chat, Task> handler)
    // {
    //     var actionBuilder = factory.OnUpdate(condition, handler);
    //
    //     return actionBuilder;
    // }
    //
    // public static IBotActionBuilder OnUpdate(this IBotActionCatalogFactory factory, Func<Update, bool> condition, Delegate handler)
    // {
    //     var actionBuilder = factory.OnUpdate(condition, handler);
    //
    //     return actionBuilder;
    // }
    //
    // public static IBotActionBuilder OnUpdate(this IBotActionCatalogFactory factory, Func<Update, bool> condition,
    //     Func<INavigatorClient, Chat, Task> handler)
    // {
    //     var actionBuilder = factory.OnUpdate(condition, handler);
    //
    //     return actionBuilder;
    // }

    public static IBotActionBuilder OnCommand(this IBotActionCatalogFactory factory, string command, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate((Update update) => update.Message?.ExtractCommand() == command, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageEntityType.BotCommand)));

        return actionBuilder;
    }
}