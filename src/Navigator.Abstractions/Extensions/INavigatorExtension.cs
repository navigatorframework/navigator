using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration.Options;

namespace Navigator.Abstractions.Extensions;

public interface INavigatorExtension
{
    void Configure(IServiceCollection services, NavigatorOptions options);
}

public interface INavigatorExtension<in TOptions> where TOptions : INavigatorExtensionOptions
{
    void Configure(IServiceCollection services, NavigatorOptions navigatorOptions, TOptions extensionOptions);
}