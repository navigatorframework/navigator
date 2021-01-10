using Telegram.Bot.Types;

namespace Navigator.Extensions.Store.Abstractions
{
    public interface IUserMapper<out TUser>
    {
        TUser Parse(User user);
    }
}