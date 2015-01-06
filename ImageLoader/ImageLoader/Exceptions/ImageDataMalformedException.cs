namespace ImageLoader.Exceptions
{
    public class ImageDataMalformedException : MyCustomException
    {
        public ImageDataMalformedException(string message) : base(message)
        { }
    }
}