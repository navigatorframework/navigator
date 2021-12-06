using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Navigator.Context;

namespace Navigator.Actions.MIddleware
{
    public class CooldownActionFilter<TAction> : IActionMiddleware<TAction> where TAction : IAction
    {
        private readonly IDistributedCache _cache;
        private readonly INavigatorContext _navigatorContext;
        public CooldownActionFilter(IDistributedCache cache, INavigatorContextAccessor navigatorContextAccessor)
        {
            _cache = cache;
            _navigatorContext = navigatorContextAccessor.NavigatorContext;
        }

        public async Task<Status> Handle(TAction action, CancellationToken cancellationToken, RequestHandlerDelegate<Status> next)
        {
            var key = GenerateKey(action);
            
            var cooldownStatus = await _cache.GetAsync<string>(key, cancellationToken);

            if (cooldownStatus is not null)
            {
                return new Status(true);
            }

            var status = await next();

            if (status.IsSuccess)
            {
                await _cache.SetStringAsync(key, key); //TODO add timer with attribute;
            }

            return status;
        }

        private string GenerateKey(TAction action)
        {
            return $"{action.GetType().GetActionType()}:{_navigatorContext.Conversation.Chat.Id}";
        }
    }
}