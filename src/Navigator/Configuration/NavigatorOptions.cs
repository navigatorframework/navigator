using MihaZupan.TelegramBotClients.RateLimitedClient;

namespace Navigator.Configuration
{
    public class NavigatorOptions
    {
        public SchedulerSettings SchedulerSettings { get; set; }
        public string BotToken { get; set; }
        public string BotWebHookUrl { get; set; }
    }
}