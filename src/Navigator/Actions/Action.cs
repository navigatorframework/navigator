using System;
using Navigator.Abstraction;

namespace Navigator.Actions
{
    public abstract class Action : IAction
    {
        public DateTime Timestamp { get; }

        protected Action()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}