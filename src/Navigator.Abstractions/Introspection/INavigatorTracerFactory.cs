namespace Navigator.Abstractions.Introspection;

public interface INavigatorTracerFactory<out TCategoryName>
{
    public bool HasActiveTrace();
    public INavigatorTracer Get(string? identifier = null);
}