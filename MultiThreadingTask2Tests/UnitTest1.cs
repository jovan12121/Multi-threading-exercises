using Moq;
using Moq.Protected;
using MultiThreadingTask2;
using System.Net;

namespace MultiThreadingTask2Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDownloadImage_Success()
        {
            // Arrange
            string imageUrl = "https://example.com/image.jpg";
            string destinationPath = "C:\\Users\\jovan\\Desktop\\";

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new ByteArrayContent(new byte[2] { 0x88, 0x32 })
               })
               .Verifiable();
            var httpClient = new HttpClient(handlerMock.Object);
            var semaphore = new SemaphoreSlim(1);
            var imageDownloader = new ImageDownloader(semaphore, httpClient);
            var path = imageDownloader.DownloadImage(imageUrl, destinationPath);

            Assert.IsTrue(File.Exists(path));
            File.Delete(path);
        }
        [TestMethod]
        public void TestDownloadImage_Failure()
        {
            string url = "https://example.com/nonexistentimage.jpg";
            string directory = "downloadedthumbnails";

            var semaphore = new SemaphoreSlim(1);
            

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ThrowsAsync(new HttpRequestException("Not Found"))
               .Verifiable();

            var downloader = new ImageDownloader(semaphore, new HttpClient(handlerMock.Object));
            downloader.DownloadImage(url, directory);

            Assert.IsFalse(File.Exists(Path.Combine(directory, "nonexistentimage.jpg")), "File should not have been downloaded.");
        }
    }
}