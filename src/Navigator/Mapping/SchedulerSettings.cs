using Navigator.Core.Abstractions;

namespace Navigator.Core.Mapping
{
    public static class SchedulerSettingsExtension
    {
        public static MihaZupan.TelegramBotClients.RateLimitedClient.SchedulerSettings ParseToRateLimited(this SchedulerSettings schedulerSettings)
        {
            return new MihaZupan.TelegramBotClients.RateLimitedClient.SchedulerSettings(
                schedulerSettings.SafeGeneralInterval, 
                schedulerSettings.SafePrivateChatInterval,
                schedulerSettings.SafeGroupChatInterval);
        }
    }
}