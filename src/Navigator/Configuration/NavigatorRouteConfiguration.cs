using Microsoft.AspNetCore.Routing;
using Navigator.Configuration.Provider;

namespace Navigator.Configuration
{
    public class NavigatorRouteConfiguration
    {
        public IEndpointRouteBuilder EndpointRouteBuilder { get; internal set; }
        
        public NavigatorRouteProviderConfiguration ForProvider { get; internal set; }

        public NavigatorRouteConfiguration(IEndpointRouteBuilder endpointRouteBuilder)
        {
            EndpointRouteBuilder = endpointRouteBuilder;

            ForProvider = new NavigatorRouteProviderConfiguration(this);
        }
    }
}