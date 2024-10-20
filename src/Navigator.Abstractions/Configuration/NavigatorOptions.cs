namespace Navigator.Configuration.Options;

/// <summary>
/// Represents all the options you can use to configure Navigator.
/// </summary>
public class NavigatorOptions
{
    private readonly Dictionary<string, object> _options;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public NavigatorOptions()
    {
        _options = new Dictionary<string, object>();
    }

    /// <summary>
    /// Registers an option, fails if the option already exists.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="option"></param>
    /// <returns></returns>
    public bool TryRegisterOption(string key, object option)
    {
        return _options.TryAdd(key, option);
    }
    
    /// <summary>
    /// Registers an option, overriding any option that may exist.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="option"></param>
    public void ForceRegisterOption(string key, object option)
    {
        _options.Remove(key);

        TryRegisterOption(key, option);
    }

    /// <summary>
    /// Retrieves an option given a key.
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    public TType? RetrieveOption<TType>(string key)
    {
        if (_options.TryGetValue(key, out var option))
        {
            return (TType) option;
        }

        return default;
    }

    /// <summary>
    /// Retrieves all options.
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, object> RetrieveAllOptions()
    {
        return _options;
    }

    /// <summary>
    /// Imports options in <see cref="Dictionary{TKey,TValue}"/> format.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="overwrite"></param>
    public void Import(Dictionary<string, object> options, bool overwrite = false)
    {
        if (overwrite)
        {
            foreach (var (key, option) in options)
            {
                ForceRegisterOption(key, option);
            }
                
            return;
        }

        foreach (var (key, option) in options)
        {
            TryRegisterOption(key, option);
        }
    }
}