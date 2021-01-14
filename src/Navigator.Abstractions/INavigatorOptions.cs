namespace Navigator.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public interface INavigatorOptions
    {
        bool TryRegisterOption(string key, object option);
        bool ForceRegisterOption(string key, object option);
        TType? RetrieveOption<TType>(string key);
    }
}