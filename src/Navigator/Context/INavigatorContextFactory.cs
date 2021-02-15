using System;
using System.Threading.Tasks;

namespace Navigator.Context
{
    public interface INavigatorContextFactory
    {
        Task Supply(Action<INavigatorContextBuilder> action);

        INavigatorContext Retrieve();
    }
}