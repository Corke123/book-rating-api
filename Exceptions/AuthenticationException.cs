using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_rating_api.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException() { }
        public AuthenticationException(string message) : base(message) { }
        public AuthenticationException(string message, Exception inner) : base(message, inner) { }
    }
}