﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Extensions.Store.Context;
using Navigator.Extensions.Store.Entity;

namespace Navigator.Extensions.Store
{
    public static class NavigatorServiceCollectionExtensions
    {
        public static IServiceCollection AddNavigatorStore(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
        {
            return services.AddNavigatorStore<NavigatorDbContext, User, Chat>(optionsAction);
        }
        public static IServiceCollection AddNavigatorStore<TContext, TUser, TChat>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
            where TContext : NavigatorDbContext<TUser, TChat>
            where TUser : User
            where TChat : Chat
        {
            services.AddDbContext<TContext>(optionsAction);

            return services;
        }
    }
}