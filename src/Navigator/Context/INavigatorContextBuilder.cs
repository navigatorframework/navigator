using System.Threading.Tasks;
using Navigator.Entities;

namespace Navigator.Context
{
    public interface INavigatorContextBuilder
    {
        INavigatorContextBuilder ForProvider(IProvider provider);
        INavigatorContextBuilder From(IUser user);

        Task<INavigatorContext> Build();
    }
}