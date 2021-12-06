using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Navigator.Actions;
using Navigator.Context;
using Navigator.Extensions.Cooldown.Extensions;

namespace Navigator.Extensions.Cooldown;

internal class CooldownActionMiddleware<TAction> : IActionMiddleware<TAction> where TAction : IAction
{
    private readonly IDistributedCache _cache;
    private readonly INavigatorContext _navigatorContext;
    
    public CooldownActionMiddleware(IDistributedCache cache, INavigatorContextAccessor navigatorContextAccessor)
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

        if (status.IsSuccess && typeof(TAction).GetCooldown() is { } cooldown)
        {
            await _cache.SetStringAsync(key, key, new DistributedCacheEntryOptions().SetAbsoluteExpiration(cooldown), cancellationToken);
        }

        return status;
    }

    private string GenerateKey(TAction action)
    {
        return $"{action.GetType().FullName}:{_navigatorContext.Conversation.Chat.Id}";
    }
}