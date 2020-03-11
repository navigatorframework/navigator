using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Navigator.Abstraction;
using Navigator.Notification;
using Telegram.Bot.Types.Enums;

namespace Navigator
{
    public class NotificationLauncher : INotificationLauncher
    {
        protected readonly ILogger<NotificationLauncher> Logger;
        protected readonly INavigatorContext Ctx;
        protected readonly IMediator Mediator;

        public NotificationLauncher(ILogger<NotificationLauncher> logger, INavigatorContext navigatorContext, IMediator mediator)
        {
            Logger = logger;
            Ctx = navigatorContext;
            Mediator = mediator;
        }

        public async Task Launch()
        {
            Logger.LogTrace("Starting with notification launch for update {UpdateId}", Ctx.Update.Id);
            
            if (Ctx.Update.Type == UpdateType.Message)
                await Mediator.Publish(new MessageNotification(Ctx.Update));
            else if (Ctx.Update.Type == UpdateType.InlineQuery)
                await Mediator.Publish(new InlineQueryNotification(Ctx.Update));
            else if (Ctx.Update.Type == UpdateType.ChosenInlineResult)
                await Mediator.Publish(new ChosenInlineResultNotification(Ctx.Update));
            else if (Ctx.Update.Type == UpdateType.CallbackQuery)
                await Mediator.Publish(new CallbackQueryNotification(Ctx.Update));
            else if (Ctx.Update.Type == UpdateType.EditedMessage)
                await Mediator.Publish(new EditedMessageNotification(Ctx.Update));
            else if (Ctx.Update.Type == UpdateType.ChannelPost)
                await Mediator.Publish(new ChannelPostNotification(Ctx.Update));
            else if (Ctx.Update.Type == UpdateType.EditedChannelPost)
                await Mediator.Publish(new EditedChannelPostNotification(Ctx.Update));
            else if (Ctx.Update.Type == UpdateType.ShippingQuery)
                await Mediator.Publish(new ShippingQueryNotification(Ctx.Update));
            else if (Ctx.Update.Type == UpdateType.PreCheckoutQuery)
                await Mediator.Publish(new PreCheckoutQueryNotification(Ctx.Update));
            else if (Ctx.Update.Type == UpdateType.Poll)
                await Mediator.Publish(new PollNotification(Ctx.Update));
            else
                Logger.LogInformation("Unknown update type received");
        }
    }
}