using Microsoft.AspNetCore.Routing;
using Navigator.Provider;

namespace Navigator
{
    public class NavigatorRouteConfiguration
    {
        public IEndpointRouteBuilder EndpointRouteBuilder { get; internal set; }
        
        public NavigatorRouteProviderConfiguration ForProvider { get; set; }

        public NavigatorRouteConfiguration(IEndpointRouteBuilder endpointRouteBuilder)
        {
            EndpointRouteBuilder = endpointRouteBuilder;

            ForProvider = new NavigatorRouteProviderConfiguration(this);
        }
        
    }
}