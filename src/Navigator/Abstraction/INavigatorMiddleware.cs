using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Navigator.Abstraction
{
    public interface INavigatorMiddleware
    {
        Task Handle(HttpRequest httpRequest);
    }
}