using System.Collections.Generic;

namespace Navigator.Context
{
    public interface INavigatorContextBuilderOptions
    {
        bool TryRegisterOption(string key, object option);
        void ForceRegisterOption(string key, object option);
        TType? RetrieveOption<TType>(string key);
        Dictionary<string, object> RetrieveAllOptions();
    }
}