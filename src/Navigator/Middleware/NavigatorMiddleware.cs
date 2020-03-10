using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Navigator.Middleware
{
    public class NavigatorMiddleware : INavigatorMiddleware
    {
        protected readonly ILogger<NavigatorMiddleware> Logger;

        public NavigatorMiddleware(ILogger<NavigatorMiddleware> logger)
        {
            Logger = logger;
        }

        public async Task Handle(HttpRequest httpRequest)
        {
            var update = await TryGetTelegramUpdate(httpRequest.Body);

            if (update == null) return;
            
            
            
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