using Navigator.Configuration;

namespace Navigator.Provider.Discord
{
    public static class NavigatorDiscordProviderOptionsExtensions
    {
        #region DiscordToken

        private const string DiscordTokenKey = "_navigator.options.discord.discord_token";

        public static void SetDiscordToken(this NavigatorDiscordProviderOptions navigatorOptions, string telegramToken)
        {
            navigatorOptions.TryRegisterOption(DiscordTokenKey, telegramToken);

        }

        public static string? GetDiscordToken(this INavigatorOptions navigatorOptions)
        {
            return navigatorOptions.RetrieveOption<string>(DiscordTokenKey);
        }
        
        #endregion
        
        #region Shards

        private const string TotalShardsKey = "_navigator.options.discord.total_shards";

        public static void SetTotalShards(this NavigatorDiscordProviderOptions navigatorOptions, int totalShards)
        {
            navigatorOptions.TryRegisterOption(TotalShardsKey, totalShards);
        }

        public static int GetTotalShardsOrDefault(this INavigatorOptions navigatorOptions)
        {
            var totalShards = navigatorOptions.RetrieveOption<int?>(TotalShardsKey);

            if (totalShards is null)
            {
                navigatorOptions.TryRegisterOption(TotalShardsKey, 1);
            }
            
            return navigatorOptions.RetrieveOption<int>(TotalShardsKey)!;
        }

        #endregion
    }
}