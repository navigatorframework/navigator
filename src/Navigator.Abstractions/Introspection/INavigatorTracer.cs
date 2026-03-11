namespace Navigator.Abstractions.Introspection;

public interface INavigatorTracer : IAsyncDisposable
{
    string Identifier { get; }

    void AddTag(string key, string value);
    void SetStatus(ENavigatorTraceStatus status, string? type, string? message);
}