using MultithreadingTask4.Classes;

namespace MultiThreadingTask4Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDownloadChunk()
        {
            var filesDownloader = new FilesDownloader();
            long startingByte = 0;
            long endingByte = 1000;
            string linkToFile = "https://example.com/testfile";
            string fileName = filesDownloader.DownloadChunk(startingByte, endingByte, linkToFile);

            Assert.IsTrue(File.Exists(fileName));
            File.Delete(fileName);
        }
        [TestMethod]
        public void TestMergeFiles()
        {

            var filesMerger = new FilesMerger();
            var files = new Dictionary<int, string>
            {
                { 0, "file1.tmp" },
                { 1, "file2.tmp" },
                { 2, "file3.tmp" }
            };

            filesMerger.MergeFiles(files);

            Assert.IsTrue(File.Exists("downloaded"));
            File.Delete("downloaded");
        }

    }
}