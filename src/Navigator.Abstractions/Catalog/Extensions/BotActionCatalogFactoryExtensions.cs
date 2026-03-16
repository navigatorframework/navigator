using System.Text.RegularExpressions;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Builder;
using Navigator.Abstractions.Actions.Builder.Extensions;
using Navigator.Abstractions.Catalog;
using Navigator.Abstractions.Priorities;
using Navigator.Abstractions.Telegram;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Passport;
using Telegram.Bot.Types.Payments;

namespace Navigator.Abstractions.Catalog.Extensions;

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
    ///         <see cref="MessageEntityType.BotCommand" /> event using the specified handler delegate. It also sets the action name to the
    ///         command string starting with <c>/</c>.
    ///     </para>
    ///     <para>
    ///         Additionally, this method can be replaced with <see cref="OnText" /> or <see cref="OnMessage" /> for more advanced
    ///         configurations, allowing for conditions to be defined not just by the command string but also by the content or structure
    ///         of the text message. This enables a wide range of customizations beyond simple command matching.
    ///     </para>
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="command">The specific command string that this action should respond to.</param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    /// <seealso cref="OnText" />
    /// <seealso cref="MessageEntityType" />
    public static IBotActionBuilder OnCommand(this IBotActionCatalogFactory factory, string command, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate((Update update) => update.Message?.ExtractCommand() == command, handler);

        actionBuilder
            .WithName($"/{command}")
            .SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageEntityType.BotCommand)))
            .SetExclusivityLevel(EExclusivityLevel.Category);

        return actionBuilder;
    }

    /// <summary>
    ///     <para>
    ///         Configures a new bot action to respond to any <see cref="MessageType" /> that includes
    ///         a <see cref="MessageEntityType.BotCommand" /> event matching the specified regular expression pattern.
    ///     </para>
    ///     <para>
    ///         The action uses the defined pattern to identify commands within incoming updates, enabling more flexible
    ///         command matching compared to exact string matches. This allows for partial matches, optional parameters,
    ///         or more advanced scenarios such as multi-word commands.
    ///     </para>
    ///     <para>
    ///         By default, command pattern actions are set with priority <see cref="EPriority.BelowNormal" />.
    ///     </para> 
    /// </summary>
    /// <param name="factory">
    ///     An instance of <see cref="IBotActionCatalogFactory" /> for adding and managing the bot action.
    /// </param>
    /// <param name="pattern">
    ///     A regular expression pattern that defines the criteria for matching commands.
    ///     The pattern is used to filter and identify the bot commands from incoming updates.
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to be executed when the regular expression pattern is matched.
    ///     Optional if it will be specified later using <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization and configuration of the bot action.
    /// </returns>
    public static IBotActionBuilder OnCommandPattern(this IBotActionCatalogFactory factory, string pattern,
        Delegate? handler = default)
    {
        var regex = new Regex(pattern, RegexOptions.Compiled);
        
        var actionBuilder = factory.OnUpdate((Update update) =>
        {
            var command = update.Message?.ExtractCommand();

            if (string.IsNullOrEmpty(command))
            {
                return false;
            }
            
            return regex.IsMatch(command);
        }, handler);

        actionBuilder
            .WithName($"/pattern:{pattern}")
            .SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageEntityType.BotCommand)))
            .WithPriority(EPriority.BelowNormal)
            .SetExclusivityLevel(EExclusivityLevel.Category);

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to any <see cref="MessageType" /> event using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    /// <seealso cref="MessageType" />
    public static IBotActionBuilder OnMessage(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.Text" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnText(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Text)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.Photo" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnPhoto(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Photo)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Audio" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnAudio(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Audio)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Video" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnVideo(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Video)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Voice" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnVoice(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Voice)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Document" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnDocument(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Document)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Sticker" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnSticker(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Sticker)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Location" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnLocation(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Location)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Contact" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnContact(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Contact)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Venue" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnVenue(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Venue)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Game" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnGame(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Game)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="VideoNote" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnVideoNote(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoNote)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Invoice" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnInvoice(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Invoice)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="SuccessfulPayment" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnSuccessfulPayment(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.SuccessfulPayment)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.ConnectedWebsite" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnConnectedWebsite(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ConnectedWebsite)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.NewChatMembers" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnNewChatMembers(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.NewChatMembers)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.LeftChatMember" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnLeftChatMember(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.LeftChatMember)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.NewChatTitle" /> events using the specified condition and
    ///     handler  delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnNewChatTitle(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.NewChatTitle)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.NewChatPhoto" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnNewChatPhoto(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.NewChatPhoto)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.PinnedMessage" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnPinnedMessage(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.PinnedMessage)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.DeleteChatPhoto" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnDeleteChatPhoto(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.DeleteChatPhoto)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.GroupChatCreated" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnGroupChatCreated(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.GroupChatCreated)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.SupergroupChatCreated" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnSupergroupChatCreated(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.SupergroupChatCreated)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.ChannelChatCreated" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChannelChatCreated(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ChannelChatCreated)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.MigrateFromChatId" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnMigrateFromChatId(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.MigrateFromChatId)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.MigrateToChatId" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnMigrateToChatId(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.MigrateToChatId)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Poll" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnPollMessage(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Poll)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Dice" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnDice(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Dice)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageAutoDeleteTimerChanged" /> events using the specified condition
    ///     and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnMessageAutoDeleteTimerChanged(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.MessageAutoDeleteTimerChanged)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ProximityAlertTriggered" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnProximityAlertTriggered(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ProximityAlertTriggered)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="WebAppData" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnWebAppData(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.WebAppData)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="VideoChatScheduled" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnVideoChatScheduled(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoChatScheduled)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="VideoChatStarted" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnVideoChatStarted(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoChatStarted)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="VideoChatEnded" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnVideoChatEnded(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoChatEnded)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="VideoChatParticipantsInvited" /> events using the specified condition
    ///     and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnVideoChatParticipantsInvited(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.VideoChatParticipantsInvited)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Animation" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnAnimation(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Animation)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ForumTopicCreated" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnForumTopicCreated(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ForumTopicCreated)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ForumTopicClosed" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnForumTopicClosed(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ForumTopicClosed)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ForumTopicReopened" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnForumTopicReopened(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ForumTopicReopened)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ForumTopicEdited" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnForumTopicEdited(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ForumTopicEdited)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="GeneralForumTopicHidden" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnGeneralForumTopicHidden(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.GeneralForumTopicHidden)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="GeneralForumTopicUnhidden" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnGeneralForumTopicUnhidden(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.GeneralForumTopicUnhidden)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="WriteAccessAllowed" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnWriteAccessAllowed(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.WriteAccessAllowed)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="UsersShared" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnUsersShared(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.UsersShared)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ChatShared" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChatShared(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ChatShared)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="PassportData" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnPassportData(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.PassportData)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="GiveawayCreated" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnGiveawayCreated(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.GiveawayCreated)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Giveaway" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnGiveaway(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Giveaway)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="GiveawayWinners" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnGiveawayWinners(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.GiveawayWinners)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="GiveawayCompleted" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnGiveawayCompleted(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.GiveawayCompleted)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.BoostAdded" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnBoostAdded(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.BoostAdded)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Message.ChatBackgroundSet" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChatBackgroundSet(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ChatBackgroundSet)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="PaidMedia" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnPaidMedia(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.PaidMedia)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="RefundedPayment" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnRefundedPayment(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.RefundedPayment)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.Unknown" /> message events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnUnknownMessage(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Unknown)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="InlineQuery" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnInlineQuery(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.InlineQuery)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="ChosenInlineResult" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChosenInlineResult(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChosenInlineResult)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="CallbackQuery" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnCallbackQuery(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.CallbackQuery)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.EditedMessage" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnEditedMessage(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.EditedMessage)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.EditedChannelPost" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnEditedChannelPost(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.EditedChannelPost)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.ShippingQuery" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnShippingQuery(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ShippingQuery)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.PreCheckoutQuery" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnPreCheckoutQuery(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.PreCheckoutQuery)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Poll" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnPollUpdate(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.Poll)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="PollAnswer" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnPollAnswer(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.PollAnswer)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.MyChatMember" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnMyChatMember(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.MyChatMember)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.ChatMember" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChatMember(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChatMember)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.ChatJoinRequest" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChatJoinRequest(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChatJoinRequest)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.MessageReaction" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnMessageReaction(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.MessageReaction)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.MessageReactionCount" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnMessageReactionCount(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.MessageReactionCount)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.ChatBoost" /> events using the specified condition and handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChatBoost(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChatBoost)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.RemovedChatBoost" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnRemovedChatBoost(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.RemovedChatBoost)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.BusinessConnection" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnBusinessConnection(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.BusinessConnection)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.BusinessMessage" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnBusinessMessage(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.BusinessMessage)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.EditedBusinessMessage" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnEditedBusinessMessage(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.EditedBusinessMessage)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.DeletedBusinessMessages" /> events using the specified condition and
    ///     handler delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnDeletedBusinessMessages(this IBotActionCatalogFactory factory, Delegate condition,
        Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.DeletedBusinessMessages)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="UpdateType.Unknown" /> update events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnUnknownUpdate(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.Unknown)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.PurchasedPaidMedia" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnPurchasedPaidMedia(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.PurchasedPaidMedia)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="Update.ChannelPost" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChannelPost(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.ChannelPost)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.Story" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnStory(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Story)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.Gift" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnGift(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Gift)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.UniqueGift" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnUniqueGift(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.UniqueGift)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.PaidMessagePriceChanged" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnPaidMessagePriceChanged(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.PaidMessagePriceChanged)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.Checklist" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChecklist(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.Checklist)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.ChecklistTasksDone" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChecklistTasksDone(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ChecklistTasksDone)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.ChecklistTasksAdded" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChecklistTasksAdded(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ChecklistTasksAdded)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.DirectMessagePriceChanged" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnDirectMessagePriceChanged(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.DirectMessagePriceChanged)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.SuggestedPostApproved" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnSuggestedPostApproved(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.SuggestedPostApproved)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.SuggestedPostApprovalFailed" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnSuggestedPostApprovalFailed(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.SuggestedPostApprovalFailed)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.SuggestedPostDeclined" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnSuggestedPostDeclined(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.SuggestedPostDeclined)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.SuggestedPostPaid" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnSuggestedPostPaid(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.SuggestedPostPaid)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.SuggestedPostRefunded" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnSuggestedPostRefunded(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.SuggestedPostRefunded)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.GiftUpgradeSent" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnGiftUpgradeSent(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.GiftUpgradeSent)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.ChatOwnerLeft" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChatOwnerLeft(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ChatOwnerLeft)));

        return actionBuilder;
    }

    /// <summary>
    ///     Configures a new bot action to respond to <see cref="MessageType.ChatOwnerChanged" /> events using the specified condition and handler
    ///     delegates.
    /// </summary>
    /// <param name="factory">An instance of <see cref="IBotActionCatalogFactory" /></param>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met. Optional if it will be specified later using
    ///     <see cref="BotActionBuilderExtensions.SetHandler" />.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="BotActionBuilder" /> that allows further customization of the bot action.
    /// </returns>
    public static IBotActionBuilder OnChatOwnerChanged(this IBotActionCatalogFactory factory, Delegate condition, Delegate? handler = default)
    {
        var actionBuilder = factory.OnUpdate(condition, handler);

        actionBuilder.SetCategory(new UpdateCategory(nameof(MessageType), nameof(MessageType.ChatOwnerChanged)));

        return actionBuilder;
    }
}