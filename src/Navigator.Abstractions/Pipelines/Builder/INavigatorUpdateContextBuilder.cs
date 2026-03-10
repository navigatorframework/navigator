using Navigator.Abstractions.Pipelines.Context;
using Telegram.Bot.Types;

namespace Navigator.Abstractions.Pipelines.Builder;

public interface INavigatorUpdateContextBuilder
{
    ValueTask<NavigatorUpdateContext> Build(Update update);
}