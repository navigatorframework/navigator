using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Navigator.Abstraction;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Navigator
{
    /// <summary>
    /// 
    /// </summary>
    public class NavigatorMiddleware : INavigatorMiddleware
    {
        /// <summary>
        /// Logger.
        /// </summary>
        protected readonly ILogger<NavigatorMiddleware> Logger;
        
        /// <summary>
        /// Notification Launcher.
        /// </summary>
        protected readonly INotificationLauncher NotificationLauncher;
        
        /// <summary>
        /// Action Launcher.
        /// </summary>
        protected readonly IActionLauncher ActionLauncher;
        
        /// <summary>
        /// Context Builder.
        /// </summary>
        protected readonly INavigatorContextBuilder NavigatorContextBuilder;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="notificationLauncher"></param>
        /// <param name="actionLauncher"></param>
        /// <param name="navigatorContextBuilder"></param>
        public NavigatorMiddleware(ILogger<NavigatorMiddleware> logger, INotificationLauncher notificationLauncher, IActionLauncher actionLauncher, INavigatorContextBuilder navigatorContextBuilder)
        {
            Logger = logger;
            NotificationLauncher = notificationLauncher;
            ActionLauncher = actionLauncher;
            NavigatorContextBuilder = navigatorContextBuilder;
        }

        /// <inheritdoc />
        public async Task Handle(HttpRequest httpRequest)
        {
            Logger.LogTrace("Parsing telegram update.");
            var update = await ParseTelegramUpdate(httpRequest.Body);
            
            if (update == null)
            {
                Logger.LogInformation("Telegram update was not parsed.");
                return;
            }

            Logger.LogTrace("Parsed telegram update with id: {UpdateId}", update.Id);
            
            await NavigatorContextBuilder.Build(update);
            await NotificationLauncher.Launch();
            await ActionLauncher.Launch();
        }

        private static async Task<Update?> ParseTelegramUpdate(Stream stream)
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