using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface INavigatorContextExtensionProvider
    { 
        int Order => 1000;
        Task<(string?, object?)> Process(Update update);
    }
}