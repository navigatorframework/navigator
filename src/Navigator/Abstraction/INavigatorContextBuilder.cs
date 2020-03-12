using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface INavigatorContextBuilder
    {
        Task Build(Update update);
    }
}