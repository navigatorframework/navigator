using System.Threading.Tasks;

namespace Navigator.Context.Extensions
{
    public interface INavigatorContextExtension
    {
        Task Extend(INavigatorContext navigatorContext, NavigatorContextBuilderOptions builderOptions);
    }
}