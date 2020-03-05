using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstraction;

namespace Navigator
{
    public static class NavigatorServiceCollectionExtensions
    {
        public static INavigatorBuilder AddNavigatorCore(this IServiceCollection services)
        {
            var builder = new NavigatorBuilder(services);

            return builder;
        }
    }
}