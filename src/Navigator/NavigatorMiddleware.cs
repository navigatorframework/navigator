using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Navigator.Abstraction;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Navigator
{
    public class NavigatorMiddleware : INavigatorMiddleware
    {
        protected readonly ILogger<NavigatorMiddleware> Logger;
        protected readonly INotificationLauncher NotificationLauncher;
        protected readonly IActionLauncher ActionLauncher;
        protected readonly INavigatorContext NavigatorContext;

        public NavigatorMiddleware(ILogger<NavigatorMiddleware> logger, INotificationLauncher notificationLauncher, IActionLauncher actionLauncher,
            INavigatorContext navigatorContext)
        {
            Logger = logger;
            NotificationLauncher = notificationLauncher;
            ActionLauncher = actionLauncher;
            NavigatorContext = navigatorContext;
        }

        public async Task Handle(HttpRequest httpRequest)
        {
            var update = await TryGetTelegramUpdate(httpRequest.Body);
            if (update == null) return;

            await NavigatorContext.Init(update);

            await NotificationLauncher.Launch();
            await ActionLauncher.Launch();
        }

        protected async Task<Update?> TryGetTelegramUpdate(Stream stream)
        {
            try
            {
                var reader = new StreamReader(stream);
                var update = JsonConvert.DeserializeObject<Update>(await reader.ReadToEndAsync());

                // TODO: Activate this when Telegram.Bot supports System.Text.Json
                // update = await JsonSerializer.DeserializeAsync<Update>(stream);

                return update.Id == default ? default : update;
            }
            catch
            {
                Logger.LogInformation("An unknown post body was received.");
                return default;
            }
        }
    }
}