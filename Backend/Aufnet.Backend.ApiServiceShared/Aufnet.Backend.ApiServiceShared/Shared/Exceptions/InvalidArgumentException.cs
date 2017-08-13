using System;
using System.Collections.Generic;
using System.Text;

namespace Aufnet.Backend.ApiServiceShared.Shared.Exceptions
{
    //This error should be caught at this project level
    public class InvalidArgumentException: Exception
    {
        public InvalidArgumentException()
        {
        }

        public InvalidArgumentException(string message) : base(message)
        {
        }
        public InvalidArgumentException(string message, Exception inner) : base(message, inner)
        {
        }

    }
}
