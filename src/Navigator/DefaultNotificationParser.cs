using System.Threading.Tasks;
using MediatR;
using Navigator.Abstraction;
using Navigator.Abstraction.Notifications;
using Telegram.Bot.Types;

namespace Navigator
{
    public class DefaultNotificationParser : INotificationParser
    {
        public async Task<INotification> Parse(Message message)
        {
            INotification result;

            if (message.EditDate != default)
            {
                result = new EditedMessageNotification
                {
                    MessageId = message.MessageId,
                    From = message.From,
                    Date = message.Date,
                    Chat = message.Chat,
                    ForwardFrom = message.ForwardFrom,
                    ForwardFromChat = message.ForwardFromChat,
                    ForwardFromMessageId = message.ForwardFromMessageId,
                    ForwardSignature = message.ForwardSignature,
                    ForwardSenderName = message.ForwardSenderName,
                    ForwardDate = message.ForwardDate,
                    ReplyToMessage = message.ReplyToMessage,
                    EditDate = message.EditDate,
                    MediaGroupId = message.MediaGroupId,
                    AuthorSignature = message.AuthorSignature,
                    Text = message.Text,
                    Entities = message.Entities,
                    CaptionEntities = message.CaptionEntities,
                    EntityValues = message.EntityValues,
                    CaptionEntityValues = message.CaptionEntityValues,
                    Audio = message.Audio,
                    Document = message.Document,
                    Animation = message.Animation,
                    Game = message.Game,
                    Photo = message.Photo,
                    Sticker = message.Sticker,
                    Video = message.Video,
                    Voice = message.Voice,
                    VideoNote = message.VideoNote,
                    Caption = message.Caption,
                    Contact = message.Contact,
                    Location = message.Location,
                    Venue = message.Venue,
                    Poll = message.Poll,
                    NewChatMembers = message.NewChatMembers,
                    LeftChatMember = message.LeftChatMember,
                    NewChatTitle = message.NewChatTitle,
                    NewChatPhoto = message.NewChatPhoto,
                    DeleteChatPhoto = message.DeleteChatPhoto,
                    GroupChatCreated = message.GroupChatCreated,
                    SupergroupChatCreated = message.SupergroupChatCreated,
                    ChannelChatCreated = message.ChannelChatCreated,
                    MigrateToChatId = message.MigrateToChatId,
                    MigrateFromChatId = message.MigrateFromChatId,
                    PinnedMessage = message.PinnedMessage,
                    Invoice = message.Invoice,
                    SuccessfulPayment = message.SuccessfulPayment,
                    ConnectedWebsite = message.ConnectedWebsite,
                    PassportData = message.PassportData,
                    ReplyMarkup = message.ReplyMarkup,
                    Type = message.Type
                };
            }
            else
            {
                result = new MessageNotification
                {
                    MessageId = message.MessageId,
                    From = message.From,
                    Date = message.Date,
                    Chat = message.Chat,
                    ForwardFrom = message.ForwardFrom,
                    ForwardFromChat = message.ForwardFromChat,
                    ForwardFromMessageId = message.ForwardFromMessageId,
                    ForwardSignature = message.ForwardSignature,
                    ForwardSenderName = message.ForwardSenderName,
                    ForwardDate = message.ForwardDate,
                    ReplyToMessage = message.ReplyToMessage,
                    EditDate = message.EditDate,
                    MediaGroupId = message.MediaGroupId,
                    AuthorSignature = message.AuthorSignature,
                    Text = message.Text,
                    Entities = message.Entities,
                    CaptionEntities = message.CaptionEntities,
                    EntityValues = message.EntityValues,
                    CaptionEntityValues = message.CaptionEntityValues,
                    Audio = message.Audio,
                    Document = message.Document,
                    Animation = message.Animation,
                    Game = message.Game,
                    Photo = message.Photo,
                    Sticker = message.Sticker,
                    Video = message.Video,
                    Voice = message.Voice,
                    VideoNote = message.VideoNote,
                    Caption = message.Caption,
                    Contact = message.Contact,
                    Location = message.Location,
                    Venue = message.Venue,
                    Poll = message.Poll,
                    NewChatMembers = message.NewChatMembers,
                    LeftChatMember = message.LeftChatMember,
                    NewChatTitle = message.NewChatTitle,
                    NewChatPhoto = message.NewChatPhoto,
                    DeleteChatPhoto = message.DeleteChatPhoto,
                    GroupChatCreated = message.GroupChatCreated,
                    SupergroupChatCreated = message.SupergroupChatCreated,
                    ChannelChatCreated = message.ChannelChatCreated,
                    MigrateToChatId = message.MigrateToChatId,
                    MigrateFromChatId = message.MigrateFromChatId,
                    PinnedMessage = message.PinnedMessage,
                    Invoice = message.Invoice,
                    SuccessfulPayment = message.SuccessfulPayment,
                    ConnectedWebsite = message.ConnectedWebsite,
                    PassportData = message.PassportData,
                    ReplyMarkup = message.ReplyMarkup,
                    Type = message.Type
                };
            }
            
            return await Task.FromResult(result);
        }

        public async Task<INotification> Parse(CallbackQuery callbackQuery)
        {
            var result = new CallbackQueryNotification
            {
                Id = callbackQuery.Id,
                From = callbackQuery.From,
                Message = callbackQuery.Message,
                InlineMessageId = callbackQuery.InlineMessageId,
                ChatInstance = callbackQuery.ChatInstance,
                Data = callbackQuery.Data,
                GameShortName = callbackQuery.GameShortName,
                IsGameQuery = callbackQuery.IsGameQuery
            };

            return await Task.FromResult(result);
        }

        public async Task<INotification> Parse(InlineQuery inlineQuery)
        {
            var result = new InlineQueryNotification
            {
                Id = inlineQuery.Id,
                From = inlineQuery.From,
                Query = inlineQuery.Query,
                Location = inlineQuery.Location,
                Offset = inlineQuery.Offset,
            };

            return await Task.FromResult(result);
        }

        public async Task<INotification> Parse(ChosenInlineResult chosenInlineResult)
        {
            var result = new ChosenInlineResultNotification
            {
                ResultId = chosenInlineResult.ResultId,
                From = chosenInlineResult.From,
                Location = chosenInlineResult.Location,
                InlineMessageId = chosenInlineResult.InlineMessageId,
                Query = chosenInlineResult.Query,
            };

            return await Task.FromResult(result);
        }

        public async Task<INotification> Parse(Update update)
        {
            var result = new UpdateNotification
            {
                Id = update.Id,
                Message = update.Message,
                EditedMessage = update.EditedMessage,
                InlineQuery = update.InlineQuery,
                ChosenInlineResult = update.ChosenInlineResult,
                CallbackQuery = update.CallbackQuery,
                ChannelPost = update.ChannelPost,
                EditedChannelPost = update.EditedChannelPost,
                ShippingQuery = update.ShippingQuery,
                PreCheckoutQuery = update.PreCheckoutQuery,
                Poll = update.Poll,
                Type = update.Type
            };

            return await Task.FromResult(result);
        }
    }
}