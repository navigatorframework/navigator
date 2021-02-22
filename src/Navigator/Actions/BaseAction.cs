using System.Threading.Tasks;
using MediatR;
using Navigator.Context;

namespace Navigator.Actions
{
    public abstract class BaseAction : IRequest
    {
        public static string Type { get; protected set; }
        protected 
        public Task Init(INavigatorContext navigatorContext)
        {
        }
    }
}