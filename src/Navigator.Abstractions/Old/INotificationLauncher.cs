using System.Threading.Tasks;

namespace Navigator.Abstractions
{
    public interface INotificationLauncher
    {
        Task Launch();
    }
}