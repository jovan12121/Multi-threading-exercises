using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using MultiThreadingTask1.Classes;
using MultiThreadingTask1.Enums;

namespace MultiThreadingTask1Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPlaylistGenerator()
        {
            PlayListGenerator playListGenerator = new PlayListGenerator();
            List<MusicType> testList = playListGenerator.GeneratePlaylist();
            Assert.IsNotNull(testList);
            Assert.AreEqual(30, testList.Count);
        }

    }
}