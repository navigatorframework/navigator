using Microsoft.AspNetCore.Routing;

namespace Navigator.Configuration.Provider;

public class NavigatorRouteProviderConfiguration
{
    private readonly NavigatorRouteConfiguration _navigatorRouteConfiguration;

    public NavigatorRouteProviderConfiguration(NavigatorRouteConfiguration navigatorRouteConfiguration)
    {
        _navigatorRouteConfiguration = navigatorRouteConfiguration;
    }

    public NavigatorRouteConfiguration Provider(Action<IEndpointRouteBuilder> routeActions)
    {
        routeActions.Invoke(_navigatorRouteConfiguration.EndpointRouteBuilder);

        return _navigatorRouteConfiguration;
    }
}