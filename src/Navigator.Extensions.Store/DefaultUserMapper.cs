using Navigator.Extensions.Store.Abstractions;
using Navigator.Extensions.Store.Abstractions.Entity;

namespace Navigator.Extensions.Store
{
    public class DefaultUserMapper : IUserMapper<User>
    {
        public User Parse(Telegram.Bot.Types.User user)
        {
            return new User
            {
                Id = user.Id,
                IsBot = user.IsBot,
                LanguageCode = user.LanguageCode,
                Username = user.Username
            };
        }
    }
}