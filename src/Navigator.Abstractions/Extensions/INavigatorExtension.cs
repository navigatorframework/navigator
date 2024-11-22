using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration.Options;

namespace Navigator.Abstractions.Extensions;

public interface INavigatorExtension
{
    void Configure(IServiceCollection services, NavigatorOptions options);
}