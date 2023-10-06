namespace Navigator.Context;

public interface INavigatorContextBuilder
{
    Task<INavigatorContext> Build(Action<INavigatorContextBuilderOptions> configurationAction);
}