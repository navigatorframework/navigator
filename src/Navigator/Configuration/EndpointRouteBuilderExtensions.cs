using Microsoft.AspNetCore.Routing;

namespace Navigator.Configuration
{
    public static class EndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Configure navigator's provider's endpoints.
        /// </summary>
        /// <param name="endpointRouteBuilder"></param>
        /// <returns></returns>
        public static NavigatorRouteConfiguration MapNavigator(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            return new(endpointRouteBuilder);
        }
    }
}