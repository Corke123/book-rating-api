using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_rating_api.Exceptions
{
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException() { }
        public BookNotFoundException(string message) : base(message) { }
        public BookNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}