using System.Collections.Generic;
using System.Linq;

namespace Navigator
{
    public static class NavigatorClientCollectionExtensions
    {
        public static INavigatorClient? GetClientFor(this IEnumerable<INavigatorClient> clients, IProvider? provider)
        {
            return clients.FirstOrDefault(client => client.IsClientFor().GetType() == provider?.GetType());
        }
    }
}