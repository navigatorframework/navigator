using System;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram
{
    public class TelegramProvider : IProvider
    {
        private readonly NavigatorTelegramClient _client;

        public TelegramProvider(NavigatorTelegramClient client)
        {
            _client = client;
        }
        
        public INavigatorClient GetClient()
        {
            return _client;
        }

        public string GetActionType(object original)
        {
            if (original is not Update)
            {
                return string.Empty;
            }

            throw new NotImplementedException();
        }
    }
}