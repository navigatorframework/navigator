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
        protected readonly INavigatorContextBuilder NavigatorContextBuilder;

        public NavigatorMiddleware(ILogger<NavigatorMiddleware> logger, INotificationLauncher notificationLauncher, IActionLauncher actionLauncher, INavigatorContextBuilder navigatorContextBuilder)
        {
            Logger = logger;
            NotificationLauncher = notificationLauncher;
            ActionLauncher = actionLauncher;
            NavigatorContextBuilder = navigatorContextBuilder;
        }

        public async Task Handle(HttpRequest httpRequest)
        {
            Logger.LogTrace("Trying to get update from request.");
            var update = await TryGetTelegramUpdate(httpRequest.Body);
            
            if (update == null)
            {
                Logger.LogTrace("Telegram update not found or corrupted.");
                return;
            }

            Logger.LogTrace("Found telegram update {UpdateId}", update.Id);
            
            await NavigatorContextBuilder.Build(update);
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
                return default;
            }
        }
    }
}