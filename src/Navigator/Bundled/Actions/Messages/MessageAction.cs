using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Navigator.Extensions.Bundled;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// A message based action.
/// </summary>
[ActionType(nameof(MessageAction))]
public abstract class MessageAction : BaseAction
{
    /// <summary>
    /// The original Message.
    /// </summary>
    public readonly Message Message;

    /// <summary>
    /// Id of the current chat.
    /// </summary>
    public readonly long ChatId;

    /// <summary>
    /// Determines if this message is a reply to another message.
    /// </summary>
    public readonly bool IsReply;

    /// <summary>
    /// Determines if this message is a forwarded message.
    /// </summary>
    public readonly bool IsForwarded;

    /// <inheritdoc />
    protected MessageAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        var update = navigatorContextAccessor.NavigatorContext.GetOriginalEvent();
        
        Message = update.Message!;
        ChatId = Context.Conversation.Chat!.Id;
        IsReply = update.Message!.ReplyToMessage is not null;
        IsForwarded = update.Message.ForwardDate is not null;
    }
}