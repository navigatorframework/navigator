using Microsoft.AspNetCore.Routing;
using Navigator.Provider;

namespace Navigator
{
    public class NavigatorRouteConfiguration
    {
        private readonly IEndpointRouteBuilder _endpointRouteBuilder;
        
        public NavigatorRouteProviderConfiguration ForProvider { get; set; }

        public NavigatorRouteConfiguration(IEndpointRouteBuilder endpointRouteBuilder)
        {
            _endpointRouteBuilder = endpointRouteBuilder;

            ForProvider = new NavigatorRouteProviderConfiguration(this);
        }
        
    }
}