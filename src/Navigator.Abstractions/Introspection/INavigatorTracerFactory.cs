namespace Navigator.Abstractions.Introspection;

public interface INavigatorTracerFactory<out TCategoryName>
{
    public INavigatorTracer Get(string? identifier = null);
}