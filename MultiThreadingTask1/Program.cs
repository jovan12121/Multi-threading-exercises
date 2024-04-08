using System;
using System.Collections.Generic;
using System.Threading;
using MultiThreadingTask1.Classes;
using MultiThreadingTask1.Enums;

namespace MultiThreadingTask1
{
    internal class Program
    {
        static MusicType musicType = MusicType.Latino;
        static ManualResetEvent playlistGeneratedEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            List<Thread> dancerThreads = new List<Thread>();

            for (int i = 0; i < 10; i++)
            {
                int dancerId = i;
                Thread dancerThread = new Thread(() => DancerThread(dancerId));
                dancerThreads.Add(dancerThread);
                dancerThread.Start();
            }
            PlayListGenerator playListGenerator = new PlayListGenerator();
            List<MusicType> playlist = playListGenerator.GeneratePlaylist();
            playlistGeneratedEvent.Set(); 

            foreach (MusicType playlistSong in playlist)
            {
                musicType = playlistSong;
                Thread.Sleep(10000);
            }

            foreach (Thread dancerThread in dancerThreads)
            {
                dancerThread.Join();
            }
        }


        static void DancerThread(int dancerId)
        {
            playlistGeneratedEvent.WaitOne();

            while (true)
            {
                switch (musicType)
                {
                    case MusicType.Hardbass:
                        Console.WriteLine($"Dancer {dancerId}: Elbow!");
                        break;
                    case MusicType.Latino:
                        Console.WriteLine($"Dancer {dancerId}: Hips.");
                        break;
                    case MusicType.Rock:
                        Console.WriteLine($"Dancer {dancerId}: Head.");
                        break;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
