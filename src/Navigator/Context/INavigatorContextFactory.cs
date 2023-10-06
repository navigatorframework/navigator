using Navigator.Context.Builder.Options;

namespace Navigator.Context;

/// <summary>
/// Factory for creating and retrieving implementations of <see cref="INavigatorContext"/>
/// </summary>
public interface INavigatorContextFactory
{
    Task Supply(Action<INavigatorContextBuilderOptions> action);

    INavigatorContext Retrieve();
}