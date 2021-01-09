using System.Collections.Generic;
using System.Threading.Tasks;
using Navigator.Actions.Abstraction;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface IActionLauncher
    {
        Task Launch();
        IEnumerable<IAction> GetActions(Update update);
        public string? GetActionType(Update update);
    }
}