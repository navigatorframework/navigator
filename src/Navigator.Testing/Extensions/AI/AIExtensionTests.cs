using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Configuration.Options;
using Navigator.Extensions.AI;
using Xunit;

namespace Navigator.Testing.Extensions.AI;

public class AIExtensionTests
{
    [Fact]
    public void ShouldThrowWhenDistributedCacheIsMissing()
    {
        var services = new ServiceCollection();
        var extension = new AIExtension();

        var action = () => extension.Configure(services, new NavigatorOptions(), TestHelpers.CreateOptions());

        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("*IDistributedCache*");
    }

    [Fact]
    public void ShouldRegisterChatContextServicesWhenDistributedCacheExists()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IDistributedCache>(new InMemoryDistributedCache());
        var extension = new AIExtension();

        extension.Configure(services, new NavigatorOptions(), TestHelpers.CreateOptions());

        services.Should().Contain(descriptor => descriptor.ServiceType == typeof(IArgumentResolver));
        services.Should().Contain(descriptor => descriptor.ServiceType == typeof(INavigatorPipelineStep));
        services.Should().Contain(descriptor => descriptor.ServiceType == typeof(global::Navigator.Extensions.AI.Services.IChatContextStore));
    }
}
