using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Navigator.Abstraction;
using Navigator.Notification;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator
{
    public class NotificationLauncher : INotificationLauncher
    {
        protected readonly ILogger<NotificationLauncher> Logger;
        protected readonly IMediator Mediator;

        public NotificationLauncher(ILogger<NotificationLauncher> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        public async Task Launch(Update update)
        {
            if (update.Type == UpdateType.Message)
                await Mediator.Publish(new MessageNotification(update));
            else if (update.Type == UpdateType.InlineQuery)
                await Mediator.Publish(new InlineQueryNotification(update));
            else if (update.Type == UpdateType.ChosenInlineResult)
                await Mediator.Publish(new ChosenInlineResultNotification(update));
            else if (update.Type == UpdateType.CallbackQuery)
                await Mediator.Publish(new CallbackQueryNotification(update));
            else if (update.Type == UpdateType.EditedMessage)
                await Mediator.Publish(new EditedMessageNotification(update));
            else if (update.Type == UpdateType.ChannelPost)
                await Mediator.Publish(new ChannelPostNotification(update));
            else if (update.Type == UpdateType.EditedChannelPost)
                await Mediator.Publish(new EditedChannelPostNotification(update));
            else if (update.Type == UpdateType.ShippingQuery)
                await Mediator.Publish(new ShippingQueryNotification(update));
            else if (update.Type == UpdateType.PreCheckoutQuery)
                await Mediator.Publish(new PreCheckoutQueryNotification(update));
            else if (update.Type == UpdateType.Poll)
                await Mediator.Publish(new PollNotification(update));
            else
                Logger.LogInformation("Unknown update type received");
        }
    }
}