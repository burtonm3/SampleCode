using ImageLoader.Models;
using ImageLoader.Providers;

namespace ImageLoader.LoadImageFromFile
{
    public class LoadImageFromFileImplementation
    {
        private readonly IImageLoaderProvider _imageLoaderProvider;

        public LoadImageFromFileImplementation(IImageLoaderProvider imageLoaderProver)
        {
            _imageLoaderProvider = imageLoaderProver;
        }

        public Image LoadImageFromFile(string filePath)
        {
            return _imageLoaderProvider.LoadImage(filePath);
        }
    }
}