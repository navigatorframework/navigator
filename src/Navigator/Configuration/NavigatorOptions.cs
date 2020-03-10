using MihaZupan.TelegramBotClients.RateLimitedClient;

namespace Navigator.Configuration
{
    public class NavigatorOptions
    {
        public SchedulerSettings SchedulerSettings { get; set; }
        public string BotToken { get; set; }
        public string BaseWebHookUrl { get; set; }
        public string EndpointWebHook { get; set; } = "bot/update";
    }
}