using Microsoft.EntityFrameworkCore;
using Navigator.Extensions.Store.Services;
using SampleWithCustomStore.Context;
using SampleWithCustomStore.Entities;

namespace SampleWithCustomStore.Extensions;

public static class INavigatorStoreExtensions
{
    public static async Task<MessageCount> GetOrCreateMessageCountAsync(this INavigatorStore<SampleCustomDbContext> store,
        long userExternalId)
    {
        var user = await store.Context.Users.FirstAsync(u => u.ExternalId == userExternalId);

        var messageCount = await store.Context.MessageCounts
            .Where(mc => mc.User.ExternalId == userExternalId)
            .FirstOrDefaultAsync();
        
        if (messageCount == null)
        {
            messageCount = new MessageCount(user);
            store.Context.MessageCounts.Add(messageCount);
            await store.Context.SaveChangesAsync();
        }
        
        return messageCount;
    }
}