using System;

namespace Catalog.Domain.Exceptions
{
    public class InvalidProductException : Exception
    {
        public InvalidProductException(string message) : base(message) { }
    }
}
