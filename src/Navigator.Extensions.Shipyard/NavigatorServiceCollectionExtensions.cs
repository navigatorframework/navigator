using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Extensions.Shipyard.Abstractions;
using Navigator.Extensions.Shipyard.Middleware;

namespace Navigator.Extensions.Shipyard
{
    /// <summary>
    /// Navigator Shipyard Services
    /// </summary>
    public static class NavigatorServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all the necessary services for shipyard to work.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddShipyard(this IServiceCollection services)
        {
            services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, ShipyardApiKeyAuthenticationHandler>(
                nameof(ShipyardApiKeyAuthenticationHandler), o => {  });

            services.AddScoped<IBotManagementService, BotManagementService>();

            return services;
        }
    }
}