using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace SynchronizingResourceAccess
{
    class Program
    {
        static int counter;
        static object conch = new object();
        static Random r = new Random();
        static string message;

        static void MethodA()
        {
            // lock (conch)
            // {
                
            // }
            try
            {
                Monitor.TryEnter(conch, TimeSpan.FromSeconds(15));

                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    message += "A";
                    Write(".");
                    Interlocked.Increment(ref counter);
                }
            }
            finally
            {
                Monitor.Exit(conch);
            }

        }

        static void MethodB()
        {
/*             lock(conch)
            {
            } */
            //We stopped using the lock because of potential deadlocks, monitor.tryenter() is safer.

            try
            {
                Monitor.TryEnter(conch, TimeSpan.FromSeconds(15));

                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    message += "B";
                    Write(".");
                    Interlocked.Increment(ref counter);
                }
            }
            finally
            {
                Monitor.Exit(conch);
            }
            
        }
        static void Main(string[] args)
        {
            WriteLine("Please wait for the tasks to complete.");
            Stopwatch watch = Stopwatch.StartNew();

            var taskA = Task.Factory.StartNew(MethodA);
            var taskB = Task.Factory.StartNew(MethodB);

            // Task[] tasks = {taskA, taskB};
            // Task.WaitAll(tasks);
            Task.WaitAll(new Task[]{taskA, taskB});
            
            WriteLine();
            WriteLine($"Results;{message}");
            WriteLine($"{watch.ElapsedMilliseconds:#,##0} elapsedmilliseconds.");
            WriteLine($"{counter} string modifications");
        }
    }
}
