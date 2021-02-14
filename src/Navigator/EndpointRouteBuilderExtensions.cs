using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Navigator.Abstractions;

namespace Navigator
{
    public static class EndpointRouteBuilderExtensions
    {
        public static NavigatorRouteConfiguration MapNavigator(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            return new NavigatorRouteConfiguration(endpointRouteBuilder);
        }
    }
}