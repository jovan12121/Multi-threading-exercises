using AsyncTask1.Classes;
using Moq;
using Moq.Protected;
using System.Net;

namespace Async1Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestDownloadImageAsync_Success()
        {
            string imageUrl = "https://example.com/image.jpg";
            string destinationPath = "image.jpg";
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(new byte[] { 0x00, 0x01, 0x02 })
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var imageDownloader = new ImageDownloader(httpClient);
            await imageDownloader.DownloadImageAsync(imageUrl, destinationPath);
            Assert.IsTrue(System.IO.File.Exists(destinationPath));
            System.IO.File.Delete(destinationPath);
        }
        [TestMethod]
        public async Task TestDownloadImageAsync_Failure()
        {
            string imageUrl = "https://example.com/nonexistentimage.jpg";
            string destinationPath = "image.jpg";
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                   .Throws(new HttpRequestException("Server error"));

            var httpClient = new HttpClient(handler.Object);
            var imageDownloader = new ImageDownloader(httpClient);
            await imageDownloader.DownloadImageAsync(imageUrl, destinationPath);
            Assert.IsFalse(System.IO.File.Exists(destinationPath));
        }
    }
}