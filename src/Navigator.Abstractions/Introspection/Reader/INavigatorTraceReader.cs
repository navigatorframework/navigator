using Navigator.Abstractions.Introspection.Sink;

namespace Navigator.Abstractions.Introspection.Reader;

public interface INavigatorTraceReader
{
    public Task<NavigatorTraceEntry?> Retrieve(string identifier);
    public Task<IReadOnlyCollection<NavigatorTraceEntry>> RetrieveAll();
    public Task<IReadOnlyCollection<NavigatorTraceEntry>> RetrieveByChatAndMessage(long chatId, int messageId);
}