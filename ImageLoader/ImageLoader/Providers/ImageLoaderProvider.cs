using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImageLoader.Exceptions;
using ImageLoader.Models;
using ImageLoader.Repository;

namespace ImageLoader.Providers
{

    /* 
    * The provider layer is where we want to handle business 
    * logic specific to loading an image into our image model.
    * This does not handle any retriving of the data. That is done by
    * _getImageRepository.GetImageData. This seperation makes it more testable
     * and extendable by allowing us to inject any data source that might implement
     * the IImageLoaderProvider.
    */

    public class ImageLoaderProvider : IImageLoaderProvider
    {
        private readonly IGetImageRepository _getImageRepository;

        public ImageLoaderProvider(IGetImageRepository getImageRepository)
        {
            _getImageRepository = getImageRepository;
        }

        public Image LoadImage(string imageName)
        {
            try
            {
                var domainImageModel = _getImageRepository.GetImageData(imageName);

                var imageRow = (int) char.GetNumericValue(domainImageModel.Dimensions.First());
                var imageColumn = (int) char.GetNumericValue(domainImageModel.Dimensions.Last());

                var imageData = domainImageModel.Data.Trim('\n', '\r')
                    .Split('\n').Select(i => i.TrimEnd('\r'))
                    .Select(i => i.Split(','))
                    .Select(i => i.Select(byte.Parse));

                var imageDataMatrix = MapTo2DArray(imageRow, imageColumn, imageData);

                return new Image
                {
                    Rows = imageRow,
                    Columns = imageColumn,
                    Data = imageDataMatrix,
                    Name = domainImageModel.Name
                };
            }
            catch (FileNotFoundException e)
            {
                /* 1. Log Log Log!
                 
                 * 2. Throwing your own exception will seperates
                 * business logic from other layers and helps keep things clear.
                 * 
                 * 3. Also again there are probably more cases that need to be thought of
                 * but the importent thing here is we dont want a catch all.
                 * */
                throw new ImageFromeFileNotFoundException("Could not find the file");
            }
            catch (IndexOutOfRangeException e)
            {
                throw new ImageDataMalformedException("The image data array has a row or column that is greater then specfied");
            }
            catch (OverflowException e)
            {
                throw new BadImageDataException("The image data array has data that is not a valid number 0 - 255");
            }
        }

        private static byte[,] MapTo2DArray(int imageRow, int imageColumn, IEnumerable<IEnumerable<byte>> imageData)
        {

            var imageDataArray = new byte[imageRow, imageColumn];

            var rowIndex = 0;
            var colIndex = 0;

            /* There is a possible exception that be throwen here.
             * I allowed it to go unhanded because the calling method is dealing with it.
             * If there were multiple functions with multiple places that an IndexOutOfRangeException could occure
             * i would of handled it in the function and also wrapped it with its own type for clearity.
             */
            foreach (var collection in imageData)
            {
                foreach (var item in collection)
                {
                    imageDataArray[rowIndex, colIndex] = item;
                    colIndex++;
                }

                colIndex = 0;
                rowIndex++;
            }

            return imageDataArray;
        }
    }
}