namespace Navigator.Context.Builder.Options;

/// <inheritdoc />
public class NavigatorContextBuilderOptions : INavigatorContextBuilderOptions
{
    private readonly Dictionary<string, object> _options;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public NavigatorContextBuilderOptions()
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
}