using Navigator.Context.Builder.Options;

namespace Navigator.Context.Builder;

public interface INavigatorContextBuilder
{
    Task<INavigatorContext> Build(Action<INavigatorContextBuilderOptions> configurationAction);
}