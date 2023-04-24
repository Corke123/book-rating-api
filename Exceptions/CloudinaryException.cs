using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_rating_api.Exceptions
{
    public class CloudinaryException : Exception
    {
        public CloudinaryException() { }
        public CloudinaryException(string message) : base(message) { }
        public CloudinaryException(string message, Exception inner) : base(message, inner) { }
    }
}