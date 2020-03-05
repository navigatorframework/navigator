namespace Navigator.Core.Abstractions
{
    public class SchedulerSettings
    {
        public readonly int SafeGeneralInterval;
        public readonly int SafePrivateChatInterval;
        public readonly int SafeGroupChatInterval;

        public SchedulerSettings(
            int safeGeneralInterval = 34,
            int safePrivateChatInterval = 1000,
            int safeGroupChatInterval = 3000)
        {
            this.SafeGeneralInterval = safeGeneralInterval;
            this.SafePrivateChatInterval = safePrivateChatInterval;
            this.SafeGroupChatInterval = safeGroupChatInterval;
        }
    }
}