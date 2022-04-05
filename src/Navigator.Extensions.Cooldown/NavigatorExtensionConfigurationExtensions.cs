using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Configuration;
using Navigator.Configuration.Extension;

namespace Navigator.Extensions.Cooldown;

public static class NavigatorExtensionConfigurationExtensions
{
    public static NavigatorConfiguration Cooldown(this NavigatorExtensionConfiguration providerConfiguration)
    {

        return providerConfiguration.Extension(
            _ => {},
            services =>
            {
                // services.Scan(source => source
                //     .FromAssemblyOf<CooldownAttribute>()
                //     .AddClasses(filter => filter.AssignableTo(typeof(IPipelineBehavior<,>)))
                //     .UsingRegistrationStrategy(RegistrationStrategy.Append)
                //     .AsImplementedInterfaces()
                //     .WithScopedLifetime());

                services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CooldownActionMiddleware<,>));
            });
    }
}