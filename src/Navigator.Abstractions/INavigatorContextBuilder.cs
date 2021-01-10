using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstractions
{
    public interface INavigatorContextBuilder
    {
        Task Build(Update update);
    }
}