namespace Navigator.Configuration.Options;

/// <inheritdoc />
public class NavigatorOptions : INavigatorOptions
{
    private readonly Dictionary<string, object> _options;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public NavigatorOptions()
    {
        _options = new Dictionary<string, object>();
    }

    /// <inheritdoc />
    public bool TryRegisterOption(string key, object option)
    {
        return _options.TryAdd(key, option);
    }

    /// <inheritdoc />
    public void ForceRegisterOption(string key, object option)
    {
        _options.Remove(key);

        TryRegisterOption(key, option);
    }

    /// <inheritdoc />
    public TType? RetrieveOption<TType>(string key)
    {
        if (_options.TryGetValue(key, out var option))
        {
            return (TType) option;
        }

        return default;
    }

    /// <inheritdoc />
    public Dictionary<string, object> RetrieveAllOptions()
    {
        return _options;
    }

    /// <inheritdoc />
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