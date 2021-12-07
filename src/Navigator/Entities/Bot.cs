using System;

namespace Navigator.Entities
{
    public abstract record Bot : User
    {
        protected Bot(string input) : base(input)
        {
        }
    }
}