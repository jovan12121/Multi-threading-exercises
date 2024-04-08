using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace MultiThreadingTask2
{
    internal class Program
    {
        static SemaphoreSlim semaphore;

        static void Main(string[] args)
        {
            string urlForObjects = "https://jsonplaceholder.typicode.com/photos";
            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync(urlForObjects).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var jsonObjects = JArray.Parse(content);

            semaphore = new SemaphoreSlim(2);
            var imageDownloader = new ImageDownloader(semaphore, new HttpClient());

            foreach (var jsonObject in jsonObjects)
            {
                Thread t = new Thread(() => imageDownloader.DownloadImage((string)jsonObject["thumbnailUrl"], "DownloadedThumbnails"));
                t.Start();
                Thread.Sleep(5000);
            }

            Console.ReadLine();
        }
    }
}