using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingTask4.Classes
{
    public class FilesDownloader
    {
        public string DownloadChunk(long startingByte, long endingByte, string linkToFile)
        {
            using (HttpClient client = new HttpClient())
            {
                var rangeHeader = new System.Net.Http.Headers.RangeHeaderValue(startingByte, endingByte);
                client.DefaultRequestHeaders.Range = rangeHeader;
                using (HttpResponseMessage response = client.GetAsync(new Uri(linkToFile)).Result)
                {
                    HttpContent content = response.Content;
                    byte[] data = content.ReadAsByteArrayAsync().Result;
                    string fileName = Path.GetTempFileName();
                    File.WriteAllBytes(fileName, data);
                    return fileName;
                }
            }
        }
        public long GetFileSize(string url)
        {
            long result = -1;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = client.Send(new HttpRequestMessage(HttpMethod.Head, new Uri(url))))
                {
                    result = (long)responseMessage.Content.Headers.ContentLength;
                }
            }

            return result;
        }
    }
}
