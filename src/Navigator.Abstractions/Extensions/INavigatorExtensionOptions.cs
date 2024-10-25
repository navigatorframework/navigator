namespace Navigator.Abstractions.Extensions;

public interface INavigatorExtensionOptions<TExtension>
    where TExtension : INavigatorExtension;

public interface INavigatorExtension<TExtension, TOptions>
    where TOptions : INavigatorExtensionOptions<TExtension>
    where TExtension : INavigatorExtension;