using System;
using System.Collections.Generic;
using System.Text;

namespace Aufnet.Backend.Services.Base.Exceptions
{
    //This error should be caught at this project level
    internal class InvalidArgumentException: Exception
    {
        internal InvalidArgumentException()
        {
        }

        internal InvalidArgumentException(string message) : base(message)
        {
        }
        internal InvalidArgumentException(string message, Exception inner) : base(message, inner)
        {
        }

    }
}
