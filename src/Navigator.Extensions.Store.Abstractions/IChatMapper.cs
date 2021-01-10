using Telegram.Bot.Types;

namespace Navigator.Extensions.Store.Abstractions
{
    public interface IChatMapper<out TChat>
    {
        TChat Parse(Chat chat);
    }
}