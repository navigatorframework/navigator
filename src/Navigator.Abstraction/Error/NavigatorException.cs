﻿using System;
using System.Runtime.Serialization;

namespace Navigator.Core.Abstractions.Error
{
    public class NavigatorException : Exception
    {
        public NavigatorException()
        {
        }

        protected NavigatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NavigatorException(string message) : base(message)
        {
        }

        public NavigatorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}