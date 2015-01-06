using System;

namespace ImageLoader.Exceptions
{
    public class MyCustomException : Exception
    {
        public MyCustomException(string message)
        {
            MyCustomMessage = message;
        }

        public string MyCustomMessage { get; set; }
    }
}