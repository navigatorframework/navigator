using System;
using System.Threading.Tasks;
using Navigator.Abstraction;
using Telegram.Bot.Types;

namespace Navigator.Extensions.Store.Provider
{
    public class UserNavigatorContextExtensionProvider : INavigatorContextExtensionProvider
    {
        public static int Order => 500;
        public Task<(string?, object?)> Process(Update update)
        {
            throw new NotImplementedException();
        }
    }
}