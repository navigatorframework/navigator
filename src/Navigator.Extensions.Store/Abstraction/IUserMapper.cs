using Telegram.Bot.Types;

namespace Navigator.Extensions.Store.Abstraction
{
    public interface IUserMapper<out TUser>
    {
        TUser Parse(User user);
    }
}