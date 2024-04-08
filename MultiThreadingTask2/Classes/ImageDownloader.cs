using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace MultiThreadingTask2
{
    public class ImageDownloader
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly HttpClient _httpClient;
        public ImageDownloader(SemaphoreSlim semaphore, HttpClient httpClient)
        {
            _semaphore = semaphore ?? throw new ArgumentNullException(nameof(semaphore));
            _httpClient = httpClient;
        }

        public string DownloadImage(string url, string directory)
        {
            _semaphore.Wait();
            try
            {
                string fileName = Path.GetFileName(new Uri(url).AbsolutePath);
                string filePath = Path.Combine(directory, fileName);

                using (var stream = _httpClient.GetStreamAsync(url).Result)
                using (var fileStream = File.Create(filePath))
                {
                    stream.CopyTo(fileStream);
                    Console.WriteLine($"Downloaded: {fileName}");

                }
                return filePath.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to download image: {ex.Message}");
                return "";
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
