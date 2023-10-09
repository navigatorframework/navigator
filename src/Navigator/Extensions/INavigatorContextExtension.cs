using Navigator.Context;
using Navigator.Context.Builder.Options;

namespace Navigator.Extensions;

public interface INavigatorContextExtension
{
    Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions);
}