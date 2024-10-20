using Navigator.Abstractions.Extensions;
using Navigator.Configuration.Options;

namespace Navigator.Configuration;

/// <summary>
///     Helper functions for configuring navigator services.
/// </summary>
public class NavigatorConfiguration
{
    private readonly List<(Type, object?)> _extensions = [];

    /// <summary>
    ///     Gets the <see cref="NavigatorOptions" /> that are being used.
    /// </summary>
    /// <value>
    ///     The <see cref="NavigatorOptions" />
    /// </value>
    public readonly NavigatorOptions Options;

    public NavigatorConfiguration()
    {
        Options = new NavigatorOptions();
    }

    public void WithExtension<TExtension>(Action<INavigatorExtensionOptions<TExtension>>? options = default)
        where TExtension : INavigatorExtension
    {
        _extensions.Add((typeof(TExtension), options));
    }
}