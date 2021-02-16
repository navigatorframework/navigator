using System.Collections.Generic;
using Navigator.Entities;

namespace Navigator.Context
{
    public class NavigatorContextBuilderOptions : INavigatorContextBuilderOptions
    {
        private readonly Dictionary<string, object> _options;

        public NavigatorContextBuilderOptions()
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
    }
}