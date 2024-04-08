using System;
using System.Threading;

namespace MultiThreadingTask3
{
    internal class Program
    {
        static ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Length > 1)
            {
                Console.WriteLine("Another instance of the application is already running.");
                return;
            }

            Thread thread1 = new Thread(Thread1Method);
            Thread thread2 = new Thread(Thread2Method);
            Thread thread3 = new Thread(Thread3Method);
            Thread thread4 = new Thread(Thread4Method);
            Thread thread5 = new Thread(Thread5Method);
            Thread thread6 = new Thread(Thread6Method);

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();
            thread6.Start();

            thread6.Join();
            thread5.Join();
            thread4.Join();
            thread3.Join();
            thread2.Join();
            thread1.Join();
        }

        static void Thread1Method()
        {
            Console.WriteLine("Thread 1 started.");
            Thread.Sleep(1000);
            Console.WriteLine("Thread 1 set signal");
            manualResetEvent.Set();
        }

        static void Thread2Method()
        {
            Console.WriteLine("Thread 2 started.");
            Thread.Sleep(2000);
            Console.WriteLine("Thread 2 set signal");
            autoResetEvent.Set();
        }

        static void Thread3Method()
        {
            Console.WriteLine("Thread 3 is waiting for a manual signal from Thread 1");
            manualResetEvent.WaitOne();
            Console.WriteLine("Thread 3 received a manual signal, continue working");
        }

        static void Thread4Method()
        {
            Console.WriteLine("Thread 4 is waiting for a manual signal from Thread 1");
            manualResetEvent.WaitOne();
            Console.WriteLine("Thread 4 received a manual signal, continue working");
        }

        static void Thread5Method()
        {
            Console.WriteLine("Thread 5 is waiting for an auto signal from Thread 2");
            autoResetEvent.WaitOne();
            Console.WriteLine("Thread 5 received an auto signal, continue working");
        }

        static void Thread6Method()
        {
            Console.WriteLine("Thread 6 is waiting for an auto signal from Thread 2");
            autoResetEvent.WaitOne();
            Console.WriteLine("Thread 6 received an auto signal, continue working");
        }
    }
}
