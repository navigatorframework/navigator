using System.Threading.Tasks;

namespace Navigator.Abstraction
{
    public interface INotificationLauncher
    {
        Task Launch();
    }
}