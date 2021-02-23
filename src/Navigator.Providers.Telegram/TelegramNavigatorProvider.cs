using System;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram
{
    public class TelegramNavigatorProvider : INavigatorProvider
    {
        private readonly NavigatorTelegramClient _client;

        public TelegramNavigatorProvider(NavigatorTelegramClient client)
        {
            _client = client;
        }
        
        public INavigatorClient GetClient()
        {
            return _client;
        }
    }
}