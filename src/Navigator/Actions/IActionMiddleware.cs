using MediatR;

namespace Navigator.Actions
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TAction"></typeparam>
    public interface IActionMiddleware<in TAction> : IPipelineBehavior<TAction, Status> where TAction : IAction
    {
    }
}