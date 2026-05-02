#pragma warning disable SKEXP0010

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Abstractions.Extensions;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Configuration.Options;
using Navigator.Extensions.AI.Resolvers;
using Navigator.Extensions.AI.Services;
using Navigator.Extensions.AI.Steps;

namespace Navigator.Extensions.AI;

public class AIExtension : INavigatorExtension<AIOptions>
{
    /// <inheritdoc />
    public void Configure(IServiceCollection services, NavigatorOptions navigatorOptions, AIOptions extensionOptions)
    {
        if (!services.Any(service => service.ServiceType == typeof(IDistributedCache)))
        {
            throw new InvalidOperationException(
                "Navigator.Extensions.AI requires an IDistributedCache registration. Register one before calling WithExtension<AIExtension>(...).");
        }

        services.AddSingleton(extensionOptions);
        services.AddTransient<AiRequestLoggingHandler>();

        RegisterHttpClient(services, extensionOptions.ChatCompletionProvider);
        RegisterHttpClient(services, extensionOptions.EmbeddingProvider);

        services.AddScoped<IChatContextMediaDownloader, ChatContextMediaDownloader>();
        services.AddScoped<IChatContextStore, ChatContextStore>();
        services.AddScoped<ISemanticKernelChatContextParser, SemanticKernelChatContextParser>();
        services.AddScoped<IArgumentResolver, ChatContextArgumentResolver>();
        services.AddScoped<INavigatorPipelineStep, StoreIncomingMessageInChatContextStep>();

        services.AddTransient<Kernel>(serviceProvider =>
        {
            var kernelBuilder = Kernel.CreateBuilder();
            var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

            if (extensionOptions.ChatCompletionProvider.IsConfigured())
            {
                var provider = extensionOptions.ChatCompletionProvider;

                kernelBuilder.AddOpenAIChatCompletion(
                    modelId: provider.ModelId,
                    apiKey: provider.ApiKey ?? string.Empty,
                    httpClient: clientFactory.CreateClient(provider.GetClientName()));
            }

            if (extensionOptions.EmbeddingProvider.IsConfigured())
            {
                var provider = extensionOptions.EmbeddingProvider;

                kernelBuilder.AddOpenAIEmbeddingGenerator(
                    modelId: provider.ModelId,
                    apiKey: provider.ApiKey ?? string.Empty,
                    httpClient: clientFactory.CreateClient(provider.GetClientName()));
            }

            return kernelBuilder.Build();
        });
    }

    private static void RegisterHttpClient(IServiceCollection services, AIOptions.AiProvider provider)
    {
        if (!provider.IsConfigured())
        {
            return;
        }

        services.AddHttpClient(provider.GetClientName(), (_, client) =>
            {
                client.BaseAddress = new Uri(provider.ApiUrl);

                if (!string.IsNullOrEmpty(provider.ApiKey))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {provider.ApiKey}");
                }
            })
            .AddHttpMessageHandler<AiRequestLoggingHandler>();
    }
}
