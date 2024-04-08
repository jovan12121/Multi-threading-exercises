using MultiThreadingTask1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadingTask1.Classes
{
    public class PlayListGenerator
    {
        public List<MusicType> GeneratePlaylist()
        {
            List<MusicType> playlist = new List<MusicType>();
            Random random = new Random();
            for (int i = 0; i < 30; i++)
            {
                int randomNumber = random.Next(0, 3);
                switch (randomNumber)
                {
                    case 0:
                        playlist.Add(MusicType.Hardbass);
                        break;
                    case 1:
                        playlist.Add(MusicType.Latino);
                        break;
                    case 2:
                        playlist.Add(MusicType.Rock);
                        break;
                }
            }
            return playlist;
        }
    }
}
