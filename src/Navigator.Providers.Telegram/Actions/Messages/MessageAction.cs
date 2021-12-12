using System.ComponentModel;
using System.Reflection;
using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Extensions;
using Navigator.Context.Extensions.Bundled;
using Navigator.Context.Extensions.Bundled.OriginalEvent;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Messages;

/// <summary>
/// A message based action.
/// </summary>
[ActionType(nameof(MessageAction))]
public abstract class MessageAction : BaseAction
{
    /// <summary>
    /// The original Message.
    /// </summary>
    public Message Message { get; protected set; }

    /// <summary>
    /// Determines if this message is a reply to another message.
    /// </summary>
    public bool IsReply { get; protected set; }

    /// <summary>
    /// Determines if this message is a forwarded message.
    /// </summary>
    public bool IsForwarded { get; protected set; }

    /// <inheritdoc />
    protected MessageAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = navigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();

        Message = update.Message!;
        IsReply = update.Message!.ReplyToMessage is not null;
        IsForwarded = update.Message.ForwardDate is not null;
    }
}