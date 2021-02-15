using System.Threading.Tasks;
using Navigator.Entities;

namespace Navigator.Context
{
    public interface INavigatorContextBuilder
    {
        Task Build(IProvider provider, IUser from, IConversation conversation);
    }
}