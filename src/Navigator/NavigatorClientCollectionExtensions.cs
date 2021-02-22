using System;
using System.Collections.Generic;
using System.Linq;

namespace Navigator
{
    public static class NavigatorClientCollectionExtensions
    {
        public static INavigatorClient? GetClientFor(this IEnumerable<IProvider> clients, Type? provider)
        {
            return clients.FirstOrDefault(client => client.IsClientFor().GetType() == provider);
        }
    }
}