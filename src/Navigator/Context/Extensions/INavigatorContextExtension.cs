using System.Threading.Tasks;

namespace Navigator.Context.Extensions;

public interface INavigatorContextExtension
{
    Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions);
}