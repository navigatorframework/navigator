namespace Navigator.Configuration;

/// <summary>
/// Represents all the options you can use to configure Navigator.
/// </summary>
public interface INavigatorOptions
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
    
    /// <summary>
    /// Imports options in <see cref="Dictionary{TKey,TValue}"/> format.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="overwrite"></param>
    void Import(Dictionary<string, object> options, bool overwrite = false);
}