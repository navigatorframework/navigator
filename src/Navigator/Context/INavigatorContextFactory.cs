using Navigator.Context.Builder.Options;

namespace Navigator.Context;

/// <summary>
/// Factory for creating and retrieving implementations of <see cref="INavigatorContext"/>
/// </summary>
public interface INavigatorContextFactory
{
    /// <summary>
    /// Supplies <see cref="INavigatorContextBuilderOptions"/> to the factory.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    Task Supply(Action<INavigatorContextBuilderOptions> action);

    /// <summary>
    /// Retrieves a finished <see cref="INavigatorContext"/>.
    /// </summary>
    /// <returns></returns>
    INavigatorContext Retrieve();
}