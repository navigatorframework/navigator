using MediatR;

namespace Navigator.Actions;

/// <summary>
/// Interface to implement middleware for actions.
/// </summary>
/// <typeparam name="TAction"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IActionMiddleware<in TAction, TResponse> : IPipelineBehavior<TAction, Status> where TAction : IAction
{
}