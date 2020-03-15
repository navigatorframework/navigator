using System;
using Navigator.Extensions.Store.Abstraction;
using Navigator.Extensions.Store.Entity;

namespace Navigator.Extensions.Store
{
    public class DefaultChatMapper : IChatMapper<Chat>
    {
        public Chat Parse(Telegram.Bot.Types.Chat chat)
        {
            return new Chat
            {
                Id = chat.Id,
                Username = chat.Username,
                Title = chat.Title,
                Type = Enum.Parse<Chat.ChatType>(chat.Type.ToString())
            };
        }
    }
}