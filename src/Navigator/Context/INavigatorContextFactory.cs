using System;
using System.Threading.Tasks;

namespace Navigator.Context
{
    public interface INavigatorContextFactory
    {
        Task Supply(Action<INavigatorContextBuilderOptions> action);

        INavigatorContext Retrieve();
    }
}