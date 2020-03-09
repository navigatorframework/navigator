using System;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration;

namespace Navigator
{
    public static class NavigatorServiceCollectionExtensions
    {
        public static IServiceCollection AddNavigatorCore(this IServiceCollection services, Action<NavigatorOptions> navigatorOptions)
        {
            throw new NotImplementedException();
        }
    }
}