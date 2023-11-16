using Navigator.Context;
using Navigator.Context.Builder.Options;

namespace Navigator.Extensions;

/// <summary>
/// Implement this interface to extend <see cref="NavigatorContext"/>
/// </summary>
public interface INavigatorContextExtension
{
    /// <summary>
    /// Extends <see cref="NavigatorContext"/>.
    /// </summary>
    /// <param name="navigatorContext"></param>
    /// <param name="builderOptions"></param>
    /// <returns></returns>
    Task<INavigatorContext> Extend(INavigatorContext navigatorContext, INavigatorContextBuilderOptions builderOptions);
}