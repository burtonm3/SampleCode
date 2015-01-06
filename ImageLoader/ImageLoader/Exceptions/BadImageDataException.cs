using System;

namespace ImageLoader.Exceptions
{
    public class BadImageDataException : MyCustomException
    {
        public BadImageDataException(string message)
            : base(message)
        {
        }
    }
}