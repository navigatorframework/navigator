using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Navigator.Abstraction
{
    public interface INavigatorBuilder
    {
        IServiceCollection ServiceCollection { get; }
        INavigatorBuilder AddBotToken(string token);
        INavigatorBuilder AddSchedulerSettings(SchedulerSettings schedulerSettings);
        INavigatorBuilder AddCustomHttpClient(HttpClient httpClient);
        INavigatorBuilder AddActionsFromAssemblyOf<TInput>();
    }
}