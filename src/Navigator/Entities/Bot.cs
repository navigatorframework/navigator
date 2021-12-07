using System;

namespace Navigator.Entities
{
    public record Bot : User
    {
        /// <inheritdoc />
        protected Bot(string input) : base(input)
        {
        }
    }
}