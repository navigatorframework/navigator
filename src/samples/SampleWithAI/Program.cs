using Incremental.Common.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Navigator;
using Navigator.Abstractions.Catalog.Extensions;
using Navigator.Abstractions.Client;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Navigator.Extensions.AI;
using Navigator.Extensions.AI.Models;
using Navigator.Extensions.AI.Services;
using Telegram.Bot;
using Chat = Navigator.Abstractions.Entities.Chat;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddCommonConfiguration();

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddNavigator(configuration =>
{
    configuration.Options.SetWebHookBaseUrl(builder.Configuration["BASE_WEBHOOK_URL"]!);
    configuration.Options.SetTelegramToken(builder.Configuration["TELEGRAM_TOKEN"]!);

    configuration.WithExtension<AIExtension, AIOptions>(options =>
    {
        options.ChatContextLength = 15;

        options.ChatCompletionProvider = new AIOptions.AiProvider
        {
            Name = "openai-chat",
            ModelId = builder.Configuration["AI_CHAT_MODEL_ID"] ?? string.Empty,
            ApiUrl = builder.Configuration["AI_API_URL"] ?? string.Empty,
            ApiKey = builder.Configuration["AI_API_KEY"],
            IsMultimodal = builder.Configuration.GetValue("AI_CHAT_IS_MULTIMODAL", false)
        };

        options.EmbeddingProvider = new AIOptions.AiProvider
        {
            Name = "openai-embeddings",
            ModelId = builder.Configuration["AI_EMBEDDING_MODEL_ID"] ?? string.Empty,
            ApiUrl = builder.Configuration["AI_API_URL"] ?? string.Empty,
            ApiKey = builder.Configuration["AI_API_KEY"]
        };
    });
});

var app = builder.Build();

var bot = app.GetBot();

bot.OnText((string _) => true, async (
    INavigatorClient client,
    Chat chat,
    ChatContext chatContext,
    ISemanticKernelChatContextParser parser,
    Kernel kernel) =>
{
    ChatHistory history = parser.Parse(chatContext);
    var chatCompletion = kernel.Services.GetRequiredService<IChatCompletionService>();

    try
    {
        var response = await chatCompletion.GetChatMessageContentAsync(history);
        await client.SendMessage(chat, response.Content ?? "I couldn't generate a response.");
        
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }

});

app.MapNavigator();

app.Run();
