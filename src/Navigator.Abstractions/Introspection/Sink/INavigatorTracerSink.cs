namespace Navigator.Abstractions.Introspection.Sink;

public interface INavigatorTracerSink
{
    public Task Store(NavigatorTrace trace);
}