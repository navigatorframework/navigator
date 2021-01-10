using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
 using Microsoft.Extensions.DependencyInjection.Extensions;
 using Microsoft.Extensions.Options;
using Navigator.Abstractions;
using Navigator.Extensions.Store.Abstraction;
 using Navigator.Extensions.Store.Configuration;
 using Navigator.Extensions.Store.Context;
using Navigator.Extensions.Store.Entity;
 using Navigator.Extensions.Store.Provider;

 namespace Navigator.Extensions.Store
{
    public static class NavigatorServiceCollectionExtensions
    {
        public static IServiceCollection AddNavigatorStore(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptions = null,
            Action<NavigatorStoreOptions> options = null)
        {
            return services.AddNavigatorStore<NavigatorDbContext, User, Chat>(dbContextOptions, options);
        }
        
        public static IServiceCollection AddNavigatorStore<TContext, TUser>(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptions = null,
            Action<NavigatorStoreOptions> options = null)
            where TContext : NavigatorDbContext<TUser, Chat> 
            where TUser : User
        {
            return services.AddNavigatorStore<TContext, TUser, Chat>(dbContextOptions, options);
        }
        
        public static IServiceCollection AddNavigatorStore<TContext, TUser, TChat>(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptions = null,
            Action<NavigatorStoreOptions> options = null) 
            where TContext : NavigatorDbContext<TUser, TChat> 
            where TUser : User
            where TChat : Chat
        {
            if (options == null)
            {
                options = navigatorStoreOptions => { };
            }
            
            services.Configure(options);
            // services.AddDbContext<NavigatorDbContext<TUser, TChat>, TContext>(dbContextOptions);
            services.AddDbContext<TContext>(dbContextOptions);

            var storeOptions = new NavigatorStoreOptions();

            options(storeOptions);
            
            if (typeof(IChatMapper<TChat>).IsAssignableFrom(storeOptions.ChatMapper))
            {
                services.AddScoped(typeof(IChatMapper<TChat>), storeOptions.ChatMapper);
            }
            else
            {
                throw new ArgumentException("TODO");
            }

            if (typeof(IUserMapper<TUser>).IsAssignableFrom(storeOptions.UserMapper))
            {
                services.AddScoped(typeof(IUserMapper<TUser>), storeOptions.UserMapper);
            }
            else
            {
                throw new ArgumentException("TODO");
            }
            
            services.AddScoped<IEntityManager<TUser, TChat>, DefaultEntityManager<TContext, TUser, TChat>>();
            services.TryAddEnumerable(ServiceDescriptor.Scoped<INavigatorContextExtensionProvider, UpdateDataContextProvider<TUser, TChat>>());
            services.TryAddEnumerable(ServiceDescriptor.Scoped<INavigatorContextExtensionProvider, DefaultUserContextProvider<TUser, TChat>>());
            services.TryAddEnumerable(ServiceDescriptor.Scoped<INavigatorContextExtensionProvider, DefaultChatContextProvider<TUser, TChat>>());
            
            return services;
        }
    }
}