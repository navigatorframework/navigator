namespace Navigator.Abstractions.Introspection;

/// <summary>
///     Helper methods for working with navigator traces.
/// </summary>
public static class NavigatorTracerExtensions
{
    /// <summary>
    ///     Marks the trace as failed using the provided exception details.
    /// </summary>
    public static void SetError(this INavigatorTracer? tracer, Exception? exception)
    {
        if (tracer is null || exception is null)
        {
            return;
        }

        tracer.SetStatus(
            ENavigatorTraceStatus.Error,
            exception.GetType().FullName ?? exception.GetType().Name,
            exception.Message);
    }
}
