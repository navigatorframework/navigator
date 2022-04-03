using Navigator.Entities;

namespace Navigator.Extensions.Store.Extractors;

public interface IDataExtractor
{
    Dictionary<string, string> From(Conversation source);
    public bool Maps(Type type);
}