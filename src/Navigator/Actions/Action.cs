using System;
using Navigator.Abstraction;

namespace Navigator.Actions
{
    public abstract class Action : IAction
    {
        public int Order { get; } = 1000;
        public abstract string Type { get; }
        public DateTime Timestamp { get; }
        
        public abstract IAction Init(INavigatorContext ctx);
        public abstract bool CanHandle(INavigatorContext ctx);

        protected Action()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}