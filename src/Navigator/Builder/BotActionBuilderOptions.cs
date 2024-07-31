namespace Navigator.Configuration;

/// <inheritdoc />
public class BotActionBuilderOptions
{
    private readonly Dictionary<string, object> _options;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public BotActionBuilderOptions()
    {
        _options = new Dictionary<string, object>();
    }

    public bool TryRegisterOption(string key, object option)
    {
        return _options.TryAdd(key, option);
    }
    
    public void ForceRegisterOption(string key, object option)
    {
        _options.Remove(key);

        TryRegisterOption(key, option);
    }

    public TType? RetrieveOption<TType>(string key)
    {
        if (_options.TryGetValue(key, out var option))
        {
            return (TType) option;
        }

        return default;
    }

    public Dictionary<string, object> RetrieveAllOptions()
    {
        return _options;
    }

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