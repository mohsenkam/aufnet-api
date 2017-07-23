using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aufnet.Backend.Api.Models
{
    public class ErrorDto
    {


        public ErrorDto(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get;}
        public string Message { get;}
    }
}
