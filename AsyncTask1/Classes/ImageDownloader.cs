using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTask1.Classes
{
    public class ImageDownloader
    {
        private HttpClient httpClient;

        public ImageDownloader(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task DownloadImageAsync(string imageUrl, string destinationPath)
        {
            try
            {
                    HttpResponseMessage response = await httpClient.GetAsync(imageUrl);
                    response.EnsureSuccessStatusCode();
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                    await File.WriteAllBytesAsync(destinationPath, imageBytes);

                    Console.WriteLine("Image downloaded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
