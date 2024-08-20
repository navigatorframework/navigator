using Navigator.Actions;
using Navigator.Actions.Builder;
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

    /// <summary>
    ///     <para>
    ///         Configures the bot action catalog to respond to any <see cref="MessageType" /> which includes one
    ///         <see cref="MessageEntityType.BotCommand" /> event using the specified handler delegate.
    ///     </para>
    ///     <para>
    ///         Additionally, this method can be replaced with <see cref="OnTextMessage" /> for more advanced configurations,
    ///         allowing for conditions to be defined not just by the command string but also by the content or structure
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
    /// <seealso cref="OnTextMessage" />
    /// <seealso cref="MessageEntityType" />
    public static BotActionBuilder OnCommand(this BotActionCatalogFactory factory, string command, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate((Update update) => update.Message?.ExtractCommand() == command, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageEntityType.BotCommand)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures the bot action catalog to respond to any <see cref="MessageType" /> event using the specified condition and handler
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
    ///     Configures the bot action catalog to respond to <see cref="MessageType.Text" /> events using the specified condition and handler
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
    public static BotActionBuilder OnTextMessage(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Text)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures the bot action catalog to respond to <see cref="MessageType.Photo" /> events using the specified condition and handler
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
    public static BotActionBuilder OnPhotoMessage(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Photo)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures the bot action catalog to respond to <see cref="MessageType.Audio" /> events using the specified condition and handler
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
    public static BotActionBuilder OnAudioMessage(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Audio)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures the bot action catalog to respond to <see cref="MessageType.Video" /> events using the specified condition and handler
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
    public static BotActionBuilder OnVideoMessage(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Video)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures the bot action catalog to respond to <see cref="MessageType.Voice" /> events using the specified condition and handler
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
    public static BotActionBuilder OnVoiceMessage(this BotActionCatalogFactory factory, Delegate condition, Delegate handler)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(MessageType), nameof(MessageType.Voice)));

        return actionBuilder;
    }
}