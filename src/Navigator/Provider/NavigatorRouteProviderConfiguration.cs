using Microsoft.Extensions.DependencyInjection;

namespace Navigator.Provider
{
    public class NavigatorRouteProviderConfiguration
    {
        private readonly NavigatorRouteConfiguration _navigatorRouteConfiguration;

        public NavigatorRouteProviderConfiguration(NavigatorRouteConfiguration navigatorRouteConfiguration)
        {
            _navigatorRouteConfiguration = navigatorRouteConfiguration;
        }
        
        /// <summary>
        /// Configure a new provider using this method.
        /// </summary>
        /// <param name="optionsAction"></param>
        /// <param name="servicesAction"></param>
        /// <returns></returns>
        public NavigatorRouteConfiguration Provider()
        {
            optionsAction?.Invoke(_navigatorConfiguration.Options);
            
            servicesAction?.Invoke(_navigatorConfiguration.Services);

            return _navigatorConfiguration;
        }
    }
}