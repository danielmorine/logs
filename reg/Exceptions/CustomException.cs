using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reg.Exceptions
{
    public class CustomException : SystemException
    {
        public CustomException(string message) : base(message) { }
    }
}
