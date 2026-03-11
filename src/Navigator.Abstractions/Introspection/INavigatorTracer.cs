namespace Navigator.Abstractions.Introspection;

public interface INavigatorTracer : IAsyncDisposable
{
    public string Identifier { get; }
}