namespace Navigator.Context.Builder.Options;

/// <summary>
/// Navigator Context Builder Options.
/// </summary>
public interface INavigatorContextBuilderOptions
{
    /// <summary>
    /// Registers an option, fails if the option already exists.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="option"></param>
    /// <returns></returns>
    bool TryRegisterOption(string key, object option);
    
    /// <summary>
    /// Registers an option, overriding any option that may exist.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="option"></param>
    void ForceRegisterOption(string key, object option);
    
    /// <summary>
    /// Retrieves an option given a key.
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    TType? RetrieveOption<TType>(string key);
    
    /// <summary>
    /// Retrieves all options.
    /// </summary>
    /// <returns></returns>
    Dictionary<string, object> RetrieveAllOptions();
}