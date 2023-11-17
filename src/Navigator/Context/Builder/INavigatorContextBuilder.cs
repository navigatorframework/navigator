using Navigator.Context.Builder.Options;

namespace Navigator.Context.Builder;

/// <summary>
/// Navigator Context Builder.
/// </summary>
public interface INavigatorContextBuilder
{
    /// <summary>
    /// Builds a <see cref="NavigatorContext"/> and returns it.
    /// </summary>
    /// <param name="configurationAction"></param>
    /// <returns></returns>
    Task<INavigatorContext> Build(Action<INavigatorContextBuilderOptions> configurationAction);
}