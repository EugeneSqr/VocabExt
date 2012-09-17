using System;

namespace VX.Service.Infrastructure.Exceptions
{
    [Serializable]
    public class ItemAlreadyExistsException : Exception
    {
        public ItemAlreadyExistsException()
        {
        }

        public ItemAlreadyExistsException(string message) : base(message)
        {
        }

        public ItemAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}