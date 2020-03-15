using Telegram.Bot.Types;

namespace Navigator.Extensions.Store.Abstraction
{
    public interface IChatMapper<out TChat>
    {
        TChat Parse(Chat chat);
    }
}