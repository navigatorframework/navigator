using System.Collections.Generic;

namespace Navigator
{
    /// <summary>
    /// Represents all the options you can use to configure the navigator framework.
    /// </summary>
    public class NavigatorOptions : INavigatorOptions
    {
        private readonly Dictionary<string, object> _options;

        public NavigatorOptions()
        {
            _options = new Dictionary<string, object>();
        }

        public bool TryRegisterOption(string key, object option)
        {
            return _options.TryAdd(key, option);
        }
        
        public bool ForceRegisterOption(string key, object option)
        {
            _options.Remove(key);

            return TryRegisterOption(key, option);
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
}