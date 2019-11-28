using Microsoft.Extensions.DependencyInjection;
using Navigator.Core;
using Navigator.Core.Abstractions;

namespace Navigator
{
    public static class NavigatorServiceCollectionExtensions
    {
        public static INavigatorBuilder AddNavigator(this IServiceCollection services)
        {
            var builder = new NavigatorBuilder(services);
    
            return builder;
        }
    }
}