using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingTask4.Classes
{
    public class FilesMerger
    {
        public  void MergeFiles(Dictionary<int, string> files)
        {
            string downloadedFileName = "downloaded";

            using (var finalFileStream = new FileStream(downloadedFileName, FileMode.Create, FileAccess.Write))
            {
                foreach (var kvp in files.OrderBy(x => x.Key))
                {
                    string fileName = kvp.Value;
                    byte[] chunkData = File.ReadAllBytes(fileName);
                    finalFileStream.Write(chunkData, 0, chunkData.Length);
                    File.Delete(fileName);
                }
            }
        }
    }
}
