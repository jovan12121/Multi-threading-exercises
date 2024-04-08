using MultithreadingTask4.Classes;

namespace MultithreadingTask4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // I didn't know how to make a progress bar 
            string linkToFile = "https://github.com/pbatard/rufus/releases/download/v4.3/rufus-4.3.exe";
            int numberOfChunks = 4;
            var threads = new Thread[numberOfChunks];

            var tempFileDictionary = new Dictionary<int, string>();
            FilesDownloader filesDownloader = new FilesDownloader();
            long fileSize = filesDownloader.GetFileSize(linkToFile);
            long chunkSize = fileSize / numberOfChunks;

            for (int i = 0; i < numberOfChunks - 1; i++)
            {
                long startByte = i * chunkSize;
                long endByte = (i + 1) * chunkSize - 1;
                int index = i;
                threads[i] = new Thread(() =>
                {
                    string fileName = filesDownloader.DownloadChunk(startByte, endByte, linkToFile);
                    lock (tempFileDictionary)
                    {
                        tempFileDictionary.Add(index, fileName);
                    }
                });
            }
            int lastNumber = numberOfChunks - 1;
            threads[numberOfChunks - 1] = new Thread((index) =>
            {
                string fileName = filesDownloader.DownloadChunk((numberOfChunks - 1) * chunkSize, fileSize, linkToFile);
                lock (tempFileDictionary)
                {
                    tempFileDictionary.Add(lastNumber, fileName);
                }
            });

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
            FilesMerger filesMerger = new FilesMerger();
            filesMerger.MergeFiles(tempFileDictionary);
        }
    }
}
