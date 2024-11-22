using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Abstractions.Client;
using Navigator.Abstractions.Entities;
using Navigator.Abstractions.Telegram;
using Navigator.Client;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Navigator.Abstractions.Entities.Chat;
using User = Navigator.Abstractions.Entities.User;

namespace Navigator.Actions.Arguments.Resolvers;

internal sealed record NavigatorEntitiesArgumentResolver : IArgumentResolver
{
    private readonly IServiceProvider _serviceProvider;

    public NavigatorEntitiesArgumentResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public ushort Priority => 10000;


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