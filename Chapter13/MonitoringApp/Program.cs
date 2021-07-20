using System;
using System.Linq;
using MonitoringLib;

namespace MonitoringApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*  Console.WriteLine("Processing ....Please Wait");
                Recorder.Start();

                // simulate a process that requires some memory resources...
                int[] largeArrayOfInts = Enumerable.Range(1, 10_000).ToArray();
                // ...and takes some time to complete
                System.Threading.Thread.Sleep(new Random().Next(5, 10) * 1000);
                Recorder.Stop(); */

            int[] numbers = Enumerable.Range(1, 50_000).ToArray();
            Recorder.Start();
            
            Console.WriteLine("Using string with +");
            string s = "";
            
            for (int i = 0; i < numbers.Length; i++)
            {
                s += numbers[i] + ", ";
            }
            Recorder.Stop();
            Recorder.Start();
            
            Console.WriteLine("Using StringBuilder");
            var builder = new System.Text.StringBuilder();
            
            for (int i = 0; i < numbers.Length; i++)
            {
                builder.Append(numbers[i]);
                builder.Append(", ");
            }
            
            Recorder.Stop();
        }
    }
}
