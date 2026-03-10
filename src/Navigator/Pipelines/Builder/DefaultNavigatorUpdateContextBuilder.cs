using Navigator.Abstractions.Pipelines.Builder;
using Navigator.Abstractions.Pipelines.Context;
using Telegram.Bot.Types;

namespace Navigator.Pipelines.Builder;

/// <summary>
///     Default implementation of <see cref="INavigatorUpdateContextBuilder"/>.
/// </summary>
public class DefaultNavigatorUpdateContextBuilder : INavigatorUpdateContextBuilder
{
    /// <inheritdoc />
    public ValueTask<NavigatorUpdateContext> Build(Update update)
    {
        var updateContext = new NavigatorUpdateContext(update);
        return ValueTask.FromResult(updateContext);
    }
}