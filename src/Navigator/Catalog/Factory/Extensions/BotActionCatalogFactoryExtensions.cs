using Navigator.Actions;
using Navigator.Actions.Builder;
using Navigator.Telegram;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Passport;
using Telegram.Bot.Types.Payments;

namespace Navigator.Catalog.Factory.Extensions;

/// <summary>
///     These extensions provide a convenient way to create bot actions to respond to different types of updates.
///     Each extension method takes a condition and handler delegate for the bot action. The condition is a delegate that determines whether
///     the bot action should be invoked for the given update, and the handler is a delegate that defines what the bot action should do when
///     the condition is met.
/// </summary>
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

    /// <summary>
    ///     <para>
    ///         Configures a new bot action to respond to any <see cref="MessageType" /> which includes one
    ///         <see cref="MessageEntityType.BotCommand" /> event using the specified handler delegate.
    ///     </para>
    ///     <para>
    ///         Additionally, this method can be replaced with <see cref="OnText" /> or <see cref="OnMessage" /> for more advanced
    ///         configurations, allowing for conditions to be defined not just by the command string but also by the content or structure
    ///         of the text message. This enables a wide range of customizations beyond simple command matching.
    ///     </para>
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="command">The specific command string that this action should respond to.</param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    /// <seealso cref="OnText" />
    /// <seealso cref="MessageEntityType" />
    public static BotActionBuilder OnCommand(this BotActionCatalogFactory factory, string command, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate((Update update) => update.Message?.ExtractCommand() == command, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageEntityType.BotCommand)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to any <see cref="MessageType" /> event using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    /// <seealso cref="MessageType" />
    public static BotActionBuilder OnMessage(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.Text" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnText(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Text)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.Photo" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnPhoto(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Photo)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Audio" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnAudio(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Audio)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Video" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnVideo(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Video)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Voice" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnVoice(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Voice)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Document" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnDocument(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Document)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Sticker" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnSticker(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Sticker)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Location" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnLocation(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Location)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Contact" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnContact(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Contact)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Venue" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnVenue(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Venue)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Game" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnGame(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Game)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="VideoNote" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnVideoNote(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoNote)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Invoice" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnInvoice(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Invoice)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="SuccessfulPayment" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnSuccessfulPayment(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.SuccessfulPayment)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.ConnectedWebsite" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnConnectedWebsite(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.ConnectedWebsite)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.NewChatMembers" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnNewChatMembers(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.NewChatMembers)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.LeftChatMember" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnLeftChatMember(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.LeftChatMember)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.NewChatTitle" /> events using the specified condition and
    ///     handler  delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnNewChatTitle(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.NewChatTitle)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.NewChatPhoto" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnNewChatPhoto(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.NewChatPhoto)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.PinnedMessage" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnPinnedMessage(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.PinnedMessage)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.DeleteChatPhoto" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnDeleteChatPhoto(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.DeleteChatPhoto)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.GroupChatCreated" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnGroupChatCreated(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.GroupChatCreated)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.SupergroupChatCreated" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnSupergroupChatCreated(this BotActionCatalogFactory factory, Delegate condition,
        Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.SupergroupChatCreated)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.ChannelChatCreated" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnChannelChatCreated(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.ChannelChatCreated)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.MigrateFromChatId" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnMigrateFromChatId(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.MigrateFromChatId)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.MigrateToChatId" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnMigrateToChatId(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.MigrateToChatId)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Poll" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnPollMessage(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Poll)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Dice" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnDice(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Dice)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageAutoDeleteTimerChanged" /> events using the specified condition
    ///     and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnMessageAutoDeleteTimerChanged(this BotActionCatalogFactory factory, Delegate condition,
        Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.MessageAutoDeleteTimerChanged)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ProximityAlertTriggered" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnProximityAlertTriggered(this BotActionCatalogFactory factory, Delegate condition,
        Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.ProximityAlertTriggered)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="WebAppData" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnWebAppData(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.WebAppData)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="VideoChatScheduled" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnVideoChatScheduled(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoChatScheduled)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="VideoChatStarted" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnVideoChatStarted(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoChatStarted)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="VideoChatEnded" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnVideoChatEnded(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoChatEnded)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="VideoChatParticipantsInvited" /> events using the specified condition
    ///     and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnVideoChatParticipantsInvited(this BotActionCatalogFactory factory, Delegate condition,
        Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoChatParticipantsInvited)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Animation" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnAnimation(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Animation)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ForumTopicCreated" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnForumTopicCreated(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.ForumTopicCreated)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ForumTopicClosed" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnForumTopicClosed(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.ForumTopicClosed)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ForumTopicReopened" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnForumTopicReopened(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.ForumTopicReopened)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ForumTopicEdited" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnForumTopicEdited(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.ForumTopicEdited)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="GeneralForumTopicHidden" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnGeneralForumTopicHidden(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.GeneralForumTopicHidden)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="GeneralForumTopicUnhidden" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnGeneralForumTopicUnhidden(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.GeneralForumTopicUnhidden)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="WriteAccessAllowed" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnWriteAccessAllowed(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.WriteAccessAllowed)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="UsersShared" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnUsersShared(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.UsersShared)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ChatShared" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnChatShared(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.ChatShared)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="PassportData" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnPassportData(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.PassportData)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="GiveawayCreated" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnGiveawayCreated(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.GiveawayCreated)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Giveaway" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnGiveaway(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Giveaway)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="GiveawayWinners" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnGiveawayWinners(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.GiveawayWinners)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="GiveawayCompleted" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnGiveawayCompleted(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.GiveawayCompleted)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.BoostAdded" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnBoostAdded(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.BoostAdded)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.ChatBackgroundSet" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnChatBackgroundSet(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.ChatBackgroundSet)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="PaidMedia" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnPaidMedia(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.PaidMedia)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="RefundedPayment" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnRefundedPayment(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.RefundedPayment)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.Unknown" /> message events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnUnknownMessage(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Unknown)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="InlineQuery" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnInlineQuery(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.InlineQuery)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ChosenInlineResult" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnChosenInlineResult(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChosenInlineResult)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="CallbackQuery" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnCallbackQuery(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.CallbackQuery)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.EditedMessage" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnEditedMessage(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.EditedMessage)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.EditedChannelPost" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnEditedChannelPost(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.EditedChannelPost)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.ShippingQuery" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnShippingQuery(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ShippingQuery)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.PreCheckoutQuery" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnPreCheckoutQuery(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.PreCheckoutQuery)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Poll" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnPollUpdate(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.Poll)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="PollAnswer" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnPollAnswer(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.PollAnswer)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.MyChatMember" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnMyChatMember(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.MyChatMember)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.ChatMember" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnChatMember(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChatMember)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.ChatJoinRequest" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnChatJoinRequest(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChatJoinRequest)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.MessageReaction" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnMessageReaction(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.MessageReaction)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.MessageReactionCount" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnMessageReactionCount(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.MessageReactionCount)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.ChatBoost" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnChatBoost(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChatBoost)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.RemovedChatBoost" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnRemovedChatBoost(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.RemovedChatBoost)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.BusinessConnection" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnBusinessConnection(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.BusinessConnection)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.BusinessMessage" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnBusinessMessage(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.BusinessMessage)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.EditedBusinessMessage" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnEditedBusinessMessage(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.EditedBusinessMessage)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.DeletedBusinessMessages" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnDeletedBusinessMessages(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.DeletedBusinessMessages)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="UpdateType.Unknown" /> update events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="BotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static BotActionBuilder OnUnknownUpdate(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.Unknown)));

        return actionBuilder;
    }
}