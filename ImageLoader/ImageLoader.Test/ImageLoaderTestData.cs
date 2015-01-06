using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using ImageLoader.LoadImageFromFile;
using ImageLoader.Models;
using ImageLoader.Providers;
using ImageLoader.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageLoader.Test
{
    /* For the sake of simplicity Im newing up my classes.
     * However if this was a real program I would be injecting 
     * my dependancys not newing them up. New is a smell!
     */

    [TestClass]
    public class ImageLoaderTestRealData
    {
        /*A test context would be better but globals will work for now.*/
        private string _imageName;
        private string _imageDimensions;
        private List<List<byte>> _imageData;
        private string _fileName;
        private string _basePath;

        [TestInitialize]
        public void Init()
        {
            _imageName = string.Empty;
            _imageDimensions = string.Empty;
            _fileName = string.Empty;
            _basePath = string.Empty;
            _fileName = "testFile.txt";
            _basePath = GetExecutingAssemblyPath();
        }


        [TestCleanup]
        public void CleanUp()
        {
            File.Delete(_basePath + "\\" + _fileName);
        }

        [TestMethod]
        public void AClientCanGetAImageFromAImageFile()
        {
            //Given a valid image file exists 
            SetupTestFile();

            //When the file is loaded
            var loadImageFromFileProvider =
             new LoadImageFromFileImplementation(
                 new ImageLoaderProvider(
                     new GetImageFromFileRepository()));

            var image = loadImageFromFileProvider.LoadImageFromFile(_basePath + "\\" + _fileName);

            //Then the client receives and image
            Assert.IsNotNull(image, "Image object should not be null");
            //And the image contains a name
            Assert.AreEqual(image.Name, _imageName);
            //And the image contains a row
            Assert.AreEqual(image.Rows, char.GetNumericValue(_imageDimensions.First()));
            //And the image contains a column
            Assert.AreEqual(image.Columns, char.GetNumericValue(_imageDimensions.Last()));
            //And the image contains image data
            ValidateImageData(image);
        }

        public void SetupTestFile()
        {

            _imageName = "test image";
            _imageDimensions = "3 5";
            _imageData = new List<List<byte>>
            {
                new List<byte> {255, 6, 65, 78, 99},
                new List<byte> {100, 25, 0, 45, 66},
                new List<byte> {88, 190, 88, 76, 50}
            };

            var imageFileDataNormalized = _imageData
                .Select(i => i.Select(j =>
                    j.ToString())
                    .Aggregate((a, b) => a + ", " + b));

            var newFileData = new List<string>
            {
                _imageName,
                _imageDimensions
            };

            newFileData.AddRange(imageFileDataNormalized);

            File.WriteAllLines(_basePath + "\\" + _fileName, newFileData);
        }

        private void ValidateImageData(Image image)
        {
            for (int i = 0; i < image.Rows; i++)
            {
                for (int j = 0; j < image.Columns; j++)
                {
                    Assert.AreEqual(image.Data[i, j], _imageData[i][j]);
                }
            }
        }

        private static string GetExecutingAssemblyPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var basePath = Path.GetDirectoryName(path);
            return basePath;
        }
    }
}
