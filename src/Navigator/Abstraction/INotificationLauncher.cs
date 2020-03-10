using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface INotificationLauncher
    {
        Task Launch(Update update);
    }
}