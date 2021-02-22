using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Navigator.Context;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram
{
    public class TelegramMiddleware
    {
        private readonly ILogger<TelegramMiddleware> _logger;
        private readonly INavigatorContextFactory _navigatorContextFactory;

        public TelegramMiddleware(ILogger<TelegramMiddleware> logger, INavigatorContextFactory navigatorContextFactory)
        {
            _logger = logger;
            _navigatorContextFactory = navigatorContextFactory;
        }

        public async Task Process(Update update)
        {
            await _navigatorContextFactory.Supply(builder =>
            {
                builder.SetProvider<TelegramProvider>();
                builder.TryRegisterOption("navigator.provider.telegram.original_update", update);
            });
        }
    }
}