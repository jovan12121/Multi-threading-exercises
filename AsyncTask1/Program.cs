
using AsyncTask1.Classes;

namespace AsyncTask1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ImageDownloader imageDownloader = new ImageDownloader(new HttpClient());
            await imageDownloader.DownloadImageAsync("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSq0Fr1oEX9PZURzf_COP6Cqd07CBLOAWDecQt_gFtdsg&s", "C:\\Users\\jovan\\Desktop\\dog.jpg");
        }
    }
}
