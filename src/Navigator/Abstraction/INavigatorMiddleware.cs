using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Navigator.Abstraction
{
    /// <summary>
    /// Middleware for handling telegram updates incoming as webhooks.
    /// </summary>
    public interface INavigatorMiddleware
    {
        /// <summary>
        /// Handles an incoming update from telegram.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        Task Handle(HttpRequest httpRequest);
    }
}