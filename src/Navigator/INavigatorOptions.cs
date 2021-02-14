using System.Collections.Generic;

namespace Navigator
{
    /// <summary>
    /// 
    /// </summary>
    public interface INavigatorOptions
    {
        bool TryRegisterOption(string key, object option);
        bool ForceRegisterOption(string key, object option);
        TType? RetrieveOption<TType>(string key);
        Dictionary<string, object> RetrieveAllOptions();
        void Import(Dictionary<string, object> options, bool overwrite = false);
    }
}