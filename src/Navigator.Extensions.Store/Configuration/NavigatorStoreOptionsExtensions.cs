using Navigator.Extensions.Store.Abstraction;

namespace Navigator.Extensions.Store.Configuration
{
    public static class NavigatorStoreOptionsExtensions
    {
        public static void SeUserMapper<TUserMapper>(this NavigatorStoreOptions options)
        {
            options.UserMapper = typeof(TUserMapper);
        }
        
        public static void SeChatMapper<TChatMapper>(this NavigatorStoreOptions options)
        {
            options.ChatMapper = typeof(TChatMapper);
        }
    }
}