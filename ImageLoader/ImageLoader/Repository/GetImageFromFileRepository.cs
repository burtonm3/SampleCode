using System.IO;
using ImageLoader.Models;

namespace ImageLoader.Repository
{
    public class GetImageFromFileRepository : IGetImageRepository
    {
        /*It tempting to put this small piece of logic into the proivider.
         * However doing it this way makes it more testable and seperates the 
         * business logic from the data layer. This is kept as basic as possible.
         * We really just want it to create a domain model.
         */
        public DomainImageModel GetImageData(string filePath)
        {
            try
            {
                var imageContainer = new DomainImageModel();

                using (var streamReader = new StreamReader(filePath))
                {
                    imageContainer.Name = streamReader.ReadLine();
                    imageContainer.Dimensions = streamReader.ReadLine();
                    imageContainer.Data = streamReader.ReadToEnd();
                }

                return imageContainer;
            }
            catch (FileNotFoundException e)
            {
                /* 1. LOG LOG LOG!!!
                 
                   2. If this was not just for show I would be catching other possible exceptions
                 * and have test cases around them. The importent thing here is we dont want a "catch all"
                 * there is almost always a better way.
                
                   3. May want a custom exception but for now I just used the system io one. 
                 * However it could give you further seperation and allows you to also have a 
                 * more custom execption framework if you wanted.
                 */

                throw e;
            }
        }
    }
}