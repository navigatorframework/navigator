using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface IActionLauncher
    {
        Task Launch(Update update);
    }
}