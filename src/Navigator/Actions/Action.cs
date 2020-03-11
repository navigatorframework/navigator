using System;
using Navigator.Abstraction;

namespace Navigator.Actions
{
    public abstract class Action : IAction
    {
        public int Order { get; } = 1000;
        public string Type { get; }
        public DateTime Timestamp { get; }

        protected Action()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}