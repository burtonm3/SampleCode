using ImageLoader.Models;

namespace ImageLoader.Providers
{
   public interface IImageLoaderProvider
    {
        Image LoadImage(string imageName);
    }
}
