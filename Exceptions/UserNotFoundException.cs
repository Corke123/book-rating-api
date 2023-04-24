using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_rating_api.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() { }
        public UserNotFoundException(string message) : base(message) { }
        public UserNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}