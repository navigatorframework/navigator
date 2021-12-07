using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
 using Microsoft.Extensions.DependencyInjection.Extensions;
using Navigator.Abstractions;
using Old.Navigator.Extensions.Store.Abstractions;
using Old.Navigator.Extensions.Store.Abstractions.Entity;
using Old.Navigator.Extensions.Store.Context;
using Old.Navigator.Extensions.Store.Provider;

namespace Old.Navigator.Extensions.Store
{
    public static class NavigatorServiceCollectionExtensions
    {
        #region Default Implementation

        public static NavigatorBuilder AddNavigatorStore(this NavigatorBuilder navigatorBuilder, Action<DbContextOptionsBuilder> dbContextOptions = default,
            Action<NavigatorOptions> options = default)
        {
            navigatorBuilder.Options.SetUserMapper<DefaultUserMapper>();
            navigatorBuilder.Options.SetChatMapper<DefaultChatMapper>();
            
            navigatorBuilder.AddNavigatorStore<NavigatorDbContext, User, Chat>(dbContextOptions, options);
            
            return navigatorBuilder;
        }

        #endregion
        
        public static NavigatorBuilder AddNavigatorStore<TContext, TUser>(this NavigatorBuilder navigatorBuilder, Action<DbContextOptionsBuilder> dbContextOptions = default,
            Action<NavigatorOptions> options = default)
            where TContext : NavigatorDbContext<TUser, Chat> 
            where TUser : User
        {
            navigatorBuilder.Options.SetChatMapper<DefaultChatMapper>();

            return navigatorBuilder.AddNavigatorStore<TContext, TUser, Chat>(dbContextOptions, options);
        }
        
        public static NavigatorBuilder AddNavigatorStore<TContext, TUser, TChat>(this NavigatorBuilder navigatorBuilder, Action<DbContextOptionsBuilder>? dbContextOptions = default,
            Action<NavigatorOptions>? options = default) 
            where TContext : NavigatorDbContext<TUser, TChat> 
            where TUser : User
            where TChat : Chat
        {
            navigatorBuilder.Options.SetUserType<TUser>();
            navigatorBuilder.Options.SetChatType<TChat>();
            
            options?.Invoke(navigatorBuilder.Options);

            navigatorBuilder.Services.AddDbContext<TContext>(dbContextOptions);

            RegisterUserMapper<TUser>(navigatorBuilder);
            
            RegisterChatMapper<TChat>(navigatorBuilder);
            
            navigatorBuilder.Services.AddScoped<IEntityManager<TUser, TChat>, DefaultEntityManager<TContext, TUser, TChat>>();
            navigatorBuilder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<INavigatorContextExtensionProvider, UpdateDataContextProvider<TUser, TChat>>());
            navigatorBuilder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<INavigatorContextExtensionProvider, DefaultUserContextProvider<TUser, TChat>>());
            navigatorBuilder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<INavigatorContextExtensionProvider, DefaultChatContextProvider<TUser, TChat>>());
            
            navigatorBuilder.RegisterOrReplaceOptions();
            
            return navigatorBuilder;
        }

        private static void RegisterUserMapper<TUser>(NavigatorBuilder navigatorBuilder) where TUser : User
        {
            if (navigatorBuilder.Options.GetUserMapper() is not null && typeof(IUserMapper<TUser>).IsAssignableFrom(navigatorBuilder.Options.GetUserMapper()))
            {
                navigatorBuilder.Services.AddScoped(typeof(IUserMapper<TUser>), navigatorBuilder.Options.GetUserMapper()!);
            }
            else
            {
                throw new ArgumentException("TODO");
            }
        }

        private static void RegisterChatMapper<TChat>(NavigatorBuilder navigatorBuilder) where TChat : Chat
        {
            if (navigatorBuilder.Options.GetChatMapper() is not null && typeof(IChatMapper<TChat>).IsAssignableFrom(navigatorBuilder.Options.GetChatMapper()))
            {
                navigatorBuilder.Services.AddScoped(typeof(IChatMapper<TChat>), navigatorBuilder.Options.GetChatMapper()!);
            }
            else
            {
                throw new ArgumentException("TODO");
            }
        }
    }
}