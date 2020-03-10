using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Navigator.Middleware
{
    public interface INavigatorMiddleware
    {
        Task Handle(HttpRequest httpRequest);
    }
}