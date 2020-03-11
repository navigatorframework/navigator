using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface INavigatorContextFactory
    {
        Task<NavigatorContext> GetContext(Update update);
    }
}