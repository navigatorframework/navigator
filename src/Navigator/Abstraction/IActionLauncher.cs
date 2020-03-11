using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface IActionLauncher
    {
        Task Launch(Update update);
        IEnumerable<IAction> GetActions(Update update);
        public string? GetActionType(Update update);
    }
}