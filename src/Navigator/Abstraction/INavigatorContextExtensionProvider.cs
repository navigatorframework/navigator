using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface INavigatorContextExtensionProvider
    {
        Task<(Type?, object?)> Process(Update update);
    }
}