using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Bundled.Extensions.Update;
using Navigator.Context.Accessor;
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
    /// Timestamp of the message creation.
    /// </summary>
    public new readonly DateTime Timestamp;
    
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
        var update = navigatorContextAccessor.NavigatorContext.GetUpdate();
        
        Message = update.Message!;
        ChatId = Context.Conversation.Chat!.Id;
        Timestamp = Message.Date;
        IsReply = update.Message!.ReplyToMessage is not null;
        IsForwarded = update.Message.ForwardDate is not null;
    }
}