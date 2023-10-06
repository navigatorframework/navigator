namespace Navigator.Context.Builder.Options;

public interface INavigatorContextBuilderOptions
{
    bool TryRegisterOption(string key, object option);
    void ForceRegisterOption(string key, object option);
    TType? RetrieveOption<TType>(string key);
    Dictionary<string, object> RetrieveAllOptions();
}