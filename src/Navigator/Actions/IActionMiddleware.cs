using MediatR;

namespace Navigator.Actions;

/// <summary>
/// TODO
/// </summary>
/// <typeparam name="TAction"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IActionMiddleware<in TAction, TResponse> : IPipelineBehavior<TAction, Status> where TAction : IAction
{
}