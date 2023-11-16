using Microsoft.AspNetCore.Routing;

namespace Navigator.Configuration;

public class NavigatorRouteConfiguration
{
    public IEndpointRouteBuilder EndpointRouteBuilder { get; internal set; }

    public NavigatorRouteConfiguration(IEndpointRouteBuilder endpointRouteBuilder)
    {
        EndpointRouteBuilder = endpointRouteBuilder;
    }
}