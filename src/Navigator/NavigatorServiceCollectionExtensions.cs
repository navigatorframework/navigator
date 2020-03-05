﻿using Microsoft.Extensions.DependencyInjection;
using Navigator.Core.Abstractions;

namespace Navigator.Core
{
    public static class NavigatorServiceCollectionExtensions
    {
        public static INavigatorBuilder AddNavigatorCore(this IServiceCollection services)
        {
            var builder = new NavigatorBuilder(services);

            return builder;
        }
    }
}