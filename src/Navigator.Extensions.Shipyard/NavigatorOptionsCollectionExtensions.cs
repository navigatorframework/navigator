using System;
using Navigator.Abstractions;
using Navigator.Extensions.Store.Abstractions.Entity;

namespace Navigator.Extensions.Shipyard
{
    public static class NavigatorOptionsCollectionExtensions
    {
        #region ShipyardApiKey

        private const string ShipyardApiKeyKey = "_navigator.extensions.shipyard.options.api_key";

        public static void SetShipyardApiKey(this INavigatorOptions navigatorOptions, string apiKey)
        {
            navigatorOptions.TryRegisterOption(ShipyardApiKeyKey, apiKey);
        }

        public static string? GetShipyardApiKey(this INavigatorOptions navigatorOptions)
        {
            return navigatorOptions.RetrieveOption<string>(ShipyardApiKeyKey);
        }

        #endregion
    }
}