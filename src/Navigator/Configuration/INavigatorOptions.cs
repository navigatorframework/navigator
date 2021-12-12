using System.Collections.Generic;

namespace Navigator.Configuration;

/// <summary>
/// Represents all the options you can use to configure Navigator.
/// </summary>
public interface INavigatorOptions
{
    bool TryRegisterOption(string key, object option);
    void ForceRegisterOption(string key, object option);
    TType? RetrieveOption<TType>(string key);
    Dictionary<string, object> RetrieveAllOptions();
    void Import(Dictionary<string, object> options, bool overwrite = false);
}