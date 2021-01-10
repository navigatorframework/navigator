using System;
using MediatR;

namespace Navigator.Abstractions
{
    public interface IAction : IRequest
    {
        int Order { get; }
        string Type { get; }
        DateTime Timestamp { get; }
        IAction Init(INavigatorContext ctx);
        bool CanHandle(INavigatorContext ctx);
    }

    public static class ActionType
    {
        public static readonly string Command = "_navigator_command";
        public static readonly string Message = "_navigator_message";
        public static readonly string CallbackQuery = "_navigator_callbackQuery";
        public static readonly string InlineQuery = "_navigator_inlineQuery";
        public static readonly string InlineResultChosen = "_navigator_inlineResultChosen";
        public static readonly string Poll = "_navigator_poll";
        public static readonly string EditedMessage = "_navigator_editedMessage";
        public static readonly string ChannelPost = "_navigator_channelPost";
        public static readonly string EditedChannelPost = "_navigator_editedChannelPost";
        public static readonly string ShippingQuery = "_navigator_shippingQuery";
        public static readonly string PreCheckoutQuery = "_navigator_preCheckoutQuery"; 
        public static readonly string ChatMembersAdded = "_navigator_message_chatMembersAdded"; 
        public static readonly string ChatMemberLeft = "_navigator_message_ChatMemberLeft"; 
        public static readonly string ChatTitleChanged = "_navigator_message_chatTitleChanged"; 
        public static readonly string ChatPhotoChanged = "_navigator_message_chatPhotoChanged"; 
        public static readonly string MessagePinned = "_navigator_message_messagePinned"; 
        public static readonly string ChatPhotoDeleted = "_navigator_message_chatPhotoDeleted"; 
        public static readonly string GroupCreated = "_navigator_message_groupCreated"; 
        public static readonly string SupergroupCreated = "_navigator_message_supergroupCreated"; 
        public static readonly string ChannelCreated = "_navigator_message_channelCreated"; 
        public static readonly string MigratedToSupergroup = "_navigator_message_migratedToSupergroup"; 
        public static readonly string MigratedFromGroup = "_navigator_message_migratedFromGroup";
        public static readonly string Unknown = "_navigator_unknown";
    }
}