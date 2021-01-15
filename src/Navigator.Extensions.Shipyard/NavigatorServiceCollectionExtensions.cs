using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Extensions.Shipyard.Abstractions;
using Navigator.Extensions.Shipyard.Middleware;
using Navigator.Extensions.Store;
using Navigator.Extensions.Store.Abstractions;
using Navigator.Extensions.Store.Abstractions.Entity;

namespace Navigator.Extensions.Shipyard
{
    /// <summary>
    /// Navigator Shipyard Services
    /// </summary>
    public static class NavigatorServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all the necessary navigatorBuilder for shipyard to work.
        /// </summary>
        /// <param name="navigatorBuilder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static NavigatorBuilder AddShipyard(this NavigatorBuilder navigatorBuilder, Action<NavigatorOptions>? options = default)
        {
            if (options is null)
            {
                throw new ArgumentException("Please configure shipyard options.");
            }
            
            options.Invoke(navigatorBuilder.Options);
            
            navigatorBuilder.RegisterOrReplaceOptions();

            navigatorBuilder.Services.AddControllers().ConfigureApplicationPartManager(m =>
                m.FeatureProviders.Add(
                    new GenericControllerFeatureProvider(navigatorBuilder.Options.GetUserType()!, navigatorBuilder.Options.GetChatType()!)
                ));
            navigatorBuilder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, ShipyardApiKeyAuthenticationHandler>(
                nameof(ShipyardApiKeyAuthenticationHandler), o => { });

            navigatorBuilder.Services.AddScoped<IBotManagementService, BotManagementService>();

            return navigatorBuilder;
        }
    }
}