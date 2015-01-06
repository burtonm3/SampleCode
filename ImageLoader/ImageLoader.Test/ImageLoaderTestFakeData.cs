using System.Linq;
using ImageLoader.Exceptions;
using ImageLoader.LoadImageFromFile;
using ImageLoader.Providers;
using ImageLoader.Test.Fake;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageLoader.Test
{
    /* A mocking framwork would help out a lot 
     * having a bunch of fake classes wouldnt 
     * always be the best way to do it. Especally
     * if you were trying to work with your DI
     */

    [TestClass]
    public class ImageLoaderTestFakeData
    {
        [TestMethod]
        public void AnImageDataMalformedExceptionisThrownWhenTheImageDataDoesNotMatchTheSpecifiedDimensions()
        {
            var CorrectExceptionWasThrown = false;

            //Given a image file that has image data that does not match the specified dimensions 
            var loadImageFromFileProvider =
                new LoadImageFromFileImplementation(
                    new ImageLoaderProvider(
                        new ImageFromFileRepositoryFakeBadImageData()));

            //When the file is loaded
            try
            {
                loadImageFromFileProvider.LoadImageFromFile("test");
            }
            catch (ImageDataMalformedException ex)
            {
                CorrectExceptionWasThrown = true;
            }

            //Then the client recieve a ImageDataMalformedException
            Assert.IsTrue(CorrectExceptionWasThrown, "Should of throw a ImageDataMalformedException");
        }

        [TestMethod]
        public void AnImageFromeFileNotFoundExceptionIsThrownWhenTheImagFileIsNotFound()
        {
            var CorrectExceptionWasThrown = false;

            //Given a image file that does not exist
            var loadImageFromFileProvider =
                new LoadImageFromFileImplementation(
                    new ImageLoaderProvider(
                        new ImageFromFileRepositoryFakeImageNotFound()));

            //When the file is loaded
            try
            {
                loadImageFromFileProvider.LoadImageFromFile("test");
            }
            catch (ImageFromeFileNotFoundException ex)
            {
                CorrectExceptionWasThrown = true;
            }

            //Then the client recieve a ImageDataMalformedException
            Assert.IsTrue(CorrectExceptionWasThrown, "Should of throw a ImageFromeFileNotFoundException");

        }

        [TestMethod]
        public void ABadImageDataExceptionIsThrownWhenTheImageFileHasDataInItThatIsGreaterThenAByte()
        {
             var CorrectExceptionWasThrown = false;

            //Given a image file that does not exist
            var loadImageFromFileProvider =
                new LoadImageFromFileImplementation(
                    new ImageLoaderProvider(
                        new ImageFromFileRepositoryFakeImageData("test", "2 2", "1,2\n\r1000,2\n\r1,2\n\r")));

            //When the file is loaded
            try
            {
                loadImageFromFileProvider.LoadImageFromFile("test");
            }
            catch (BadImageDataException ex)
            {
                CorrectExceptionWasThrown = true;
            }

            //Then the client recieve a ImageDataMalformedException
            Assert.IsTrue(CorrectExceptionWasThrown, "Should of throw a ImageFromeFileNotFoundException");
        }
    }
}