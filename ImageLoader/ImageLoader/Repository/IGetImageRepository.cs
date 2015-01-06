using ImageLoader.Models;

namespace ImageLoader.Repository
{
    public interface IGetImageRepository
    {
        DomainImageModel GetImageData(string imageName);
    }
}