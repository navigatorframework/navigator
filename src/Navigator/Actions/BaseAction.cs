using System.Threading.Tasks;
using Navigator.Context;

namespace Navigator.Actions
{
    public abstract class BaseAction : IAction
    {
        public abstract string Type { get; protected set; }
        
        public abstract ushort Priority { get; protected set; }

        public abstract Task Init(INavigatorContext navigatorContext);
    }
}