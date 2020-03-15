using System;

namespace Navigator.Extensions.Store.Configuration
{
    public class NavigatorStoreOptions
    {
        public Type ChatMapper { get; set; } = typeof(DefaultChatMapper);
        public Type UserMapper { get; set; } = typeof(DefaultUserMapper);
    }
}