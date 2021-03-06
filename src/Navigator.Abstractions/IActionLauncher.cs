﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstractions
{
    public interface IActionLauncher
    {
        Task Launch();
        IEnumerable<IAction> GetActions(Update update);
        public string? GetActionType(Update update);
    }
}