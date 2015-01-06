using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageLoader.Models;
using ImageLoader.Repository;

namespace ImageLoader.Test.Fake
{
    public class ImageFromFileRepositoryFakeBadImageData : IGetImageRepository
    {
        public DomainImageModel GetImageData(string imagename)
        {
            return new DomainImageModel
            {
                Dimensions = "2 2",
                Name = "TestImage",
                Data = "1,2\n\r1,2\n\r1,2\n\r"
            };
        }
    }

    public class ImageFromFileRepositoryFakeImageNotFound : IGetImageRepository
    {
        public DomainImageModel GetImageData(string imagename)
        {
            throw new FileNotFoundException();
        }
    }

    public class ImageFromFileRepositoryFakeImageData : IGetImageRepository
    {
        private readonly string _name;
        private readonly string _dimensions;
        private readonly string _data;

        public ImageFromFileRepositoryFakeImageData(string name, string dimensions, string data)
        {
            _name = name;
            _dimensions = dimensions;
            _data = data;
        }

        public DomainImageModel GetImageData(string imageName)
        {
            return new DomainImageModel
            {
                Dimensions = _dimensions,
                Name = _name,
                Data = _data
            };
        }
    }
}
