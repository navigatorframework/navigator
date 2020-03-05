using System.Net.Http;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Navigator.Core.Abstractions;
using Navigator.Core.Hosted;
using Navigator.Core.Mapping;
using Scrutor;
using Telegram.Bot.Types;
using SchedulerSettings = Navigator.Core.Abstractions.SchedulerSettings;

namespace Navigator.Core
{
    public class NavigatorBuilder : INavigatorBuilder
    {
        public IServiceCollection ServiceCollection { get; }
        private readonly NavigatorClientConfiguration _navigatorClientConfiguration;

        public NavigatorBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
            _navigatorClientConfiguration = new NavigatorClientConfiguration();
            
            ServiceCollection.AddSingleton(_navigatorClientConfiguration);
            ServiceCollection.AddSingleton<INavigatorClient, NavigatorClient>();
            ServiceCollection.AddHostedService<NavigatorBackgroundService>();
            
            
            ServiceCollection.AddScoped<INavigatorActionService, NavigatorActionService>();
            ServiceCollection.AddScoped<INavigatorService, NavigatorService>();
            ServiceCollection.AddScoped<INavigatorContext, NavigatorContext<User, Chat>>();
            
            ServiceCollection.AddScoped<IChatHandler<Chat>, DefaultChatHandler>();
            ServiceCollection.AddScoped<IUserHandler<User>, DefaultUserHandler>();

            ServiceCollection.AddScoped<INotificationParser, DefaultNotificationParser>();
        }

        public INavigatorBuilder AddBotToken(string token)
        {
            _navigatorClientConfiguration.BotToken = token;

            return this;
        }

        public INavigatorBuilder AddSchedulerSettings(SchedulerSettings schedulerSettings)
        {
            _navigatorClientConfiguration.SchedulerSettings = schedulerSettings.ParseToRateLimited();

            return this;
        }
        
        public INavigatorBuilder AddCustomHttpClient(HttpClient httpClient)
        {
            _navigatorClientConfiguration.HttpClient = httpClient;

            return this;
        }

        public INavigatorBuilder AddActionsFromAssemblyOf<TInput>()
        {
            ServiceCollection.Scan(scan => scan
                .FromAssemblyOf<TInput>()
                .AddClasses(classes => classes.AssignableTo<INotification>())
                .UsingRegistrationStrategy(RegistrationStrategy.Append)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return this;
        }
    }
}