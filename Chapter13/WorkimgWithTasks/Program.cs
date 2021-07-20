using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WorkimgWithTasks
{
    class Program
    {

        static void MethodA()
        {
            Console.WriteLine("Starting Method A...");
            Thread.Sleep(3000);
            Console.WriteLine("Finishing Method A");
        }
        static void MethodB()
        {
            Console.WriteLine("Starting method B");
            Thread.Sleep(2000);
            Console.WriteLine("Finishing Method B");
        }
        static void MethodC()
        {
            Console.WriteLine("Starting Method C");
            Thread.Sleep(1000);
            Console.WriteLine("Finishing Method C");
        }
        static void Main(string[] args)
        {
            var timer = Stopwatch.StartNew();
            // Console.WriteLine("Running methods synchronously on one thread.");
            // MethodA();
            // MethodB();
            // MethodC();
            // Console.WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
            /* 
                        Console.WriteLine("Running methods asynchronously on multiple threads.");
                        Task taskA = new Task(MethodA);
                        taskA.Start();
                        Task taskB = Task.Factory.StartNew(MethodB);
                        Task taskC = Task.Run(new Action(MethodC));

                        Task[] tasks = {taskA, taskB, taskC};
                        Task.WaitAll(tasks); */

            Console.WriteLine("Passing the result of one task as an input into another.");

            var takeCallWebServiceAndThenStoredProcedure = Task.Factory.StartNew(CallWebService)
                .ContinueWith(previousTask => CallStoredProcedure(previousTask.Result));

            Console.WriteLine($"Result: {takeCallWebServiceAndThenStoredProcedure.Result}");
            Console.WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
        }

        static decimal CallWebService()
        {
            Console.WriteLine("Starting call to web service...");
            Thread.Sleep((new Random()).Next(2000, 4000));
            Console.WriteLine("Finishing call to web service");
            return 89.99M;
        }
        static string CallStoredProcedure(decimal amount)
        {
            Console.WriteLine("Starting call to stored procedure");
            Thread.Sleep((new Random()).Next(2000,4000));
            Console.WriteLine("Finishing call to stored procedure");
            return $"12 products cost more than {amount:c}";
        }
    }
}
