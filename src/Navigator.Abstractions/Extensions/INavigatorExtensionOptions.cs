using Navigator.Configuration.Options;

namespace Navigator.Abstractions.Extensions;

/// <summary>
/// Base interface for extension-specific options
/// </summary>
public interface INavigatorExtensionOptions
{
}

// /// <summary>
// /// Base class for extension options that provides common functionality
// /// </summary>
// public abstract class ExtensionOptions<TExtension> : INavigatorExtensionOptions
//     where TExtension : INavigatorExtension
// {
//     /// <summary>
//     /// Applies the extension options to the NavigatorOptions
//     /// </summary>
//     /// <param name="navigatorOptions">The global navigator options</param>
//     public abstract void Apply(NavigatorOptions navigatorOptions);
//     
//     /// <summary>
//     /// Gets the key used to store this extension's options in NavigatorOptions
//     /// </summary>
//     protected static string GetOptionsKey()
//     {
//         return $"_navigator.extensions.{typeof(TExtension).Name.ToLowerInvariant()}.options";
//     }
// }

/// <summary>
/// Extension methods for working with extension-specific options
/// </summary>
public static class NavigatorOptionsExtensionExtensions
{
    public static TOptions? GetExtensionOptions<TExtension, TOptions>(this NavigatorOptions navigatorOptions)
        where TExtension : INavigatorExtension<TOptions>
        where TOptions : INavigatorExtensionOptions
    {
        var key = GetExtensionOptionsKey<TExtension, TOptions>();
        return navigatorOptions.RetrieveOption<TOptions>(key);
    }
    
    public static void SetExtensionOptions<TExtension, TOptions>(this NavigatorOptions navigatorOptions, TOptions options)
        where TExtension : INavigatorExtension<TOptions>
        where TOptions : INavigatorExtensionOptions
    {
        var key = GetExtensionOptionsKey<TExtension, TOptions>();
        navigatorOptions.ForceRegisterOption(key, options);
    }
    
    private static string GetExtensionOptionsKey<TExtension, TOptions>()
        where TExtension : INavigatorExtension<TOptions>
        where TOptions : INavigatorExtensionOptions
    {
        return $"_navigator.extensions.{typeof(TExtension).Name.ToLowerInvariant()}.options";
    }
}