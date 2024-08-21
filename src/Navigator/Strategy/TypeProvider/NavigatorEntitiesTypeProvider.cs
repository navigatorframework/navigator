using Microsoft.Extensions.DependencyInjection;
using Navigator.Actions;
using Navigator.Client;
using Navigator.Entities;
using Navigator.Telegram;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Navigator.Entities.Chat;
using User = Navigator.Entities.User;

namespace Navigator.Strategy.TypeProvider;

internal sealed record NavigatorEntitiesTypeProvider : IArgumentTypeProvider
{
    private readonly IServiceProvider _serviceProvider;

    public NavigatorEntitiesTypeProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public ushort Priority { get; } = 10000;


    /// <inheritdoc />
    public async ValueTask<object?> GetArgument(Type type, Update update, BotAction action)
    {
        return type switch
        {
            not null when type == typeof(Conversation)
                => update.GetConversation(),
            not null when type == typeof(Chat)
                => update.GetConversation().Chat,
            not null when type == typeof(User)
                => update.GetConversation().User,
            not null when type == typeof(Bot)
                => await _serviceProvider.GetRequiredService<INavigatorClient>().GetProfile(),
            not null when type == typeof(string[]) && action.Information.Category.Subkind == nameof(MessageEntityType.BotCommand)
                => update.Message!.ExtractArguments(),
            _ => default
        };
    }
}