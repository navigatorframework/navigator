using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Navigator.Abstraction;
using Telegram.Bot.Types;

namespace Navigator.Middleware
{
    public class NavigatorMiddleware : INavigatorMiddleware
    {
        protected readonly ILogger<NavigatorMiddleware> Logger;
        protected readonly INotificationLauncher NotificationLauncher;
        protected readonly IMediator Mediator;

        public NavigatorMiddleware(ILogger<NavigatorMiddleware> logger, INotificationLauncher notificationLauncher)
        {
            Logger = logger;
            NotificationLauncher = notificationLauncher;
        }

        public async Task Handle(HttpRequest httpRequest)
        {
            var update = await TryGetTelegramUpdate(httpRequest.Body);

            if (update == null) return;

            await NotificationLauncher.Launch(update);
        }
        
        protected async Task<Update?> TryGetTelegramUpdate(Stream stream)
        {
            Update update = null;
            try
            {
                update = await JsonSerializer.DeserializeAsync<Update>(stream);
            }
            catch
            {
                Logger.LogInformation("An unknown post body was received.");
                return update;
            }

            return update;
        }
    }
}