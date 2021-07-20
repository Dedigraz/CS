using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;
namespace NestedAndChildTasks
{
    class Program
    {
        static void OuterMethod()
        {
            WriteLine("Starting outer method");
            // var inner = Task.Factory.StartNew(InnerMethod); //Was defunct, console app could finish before inner task even started
            var inner = Task.Factory.StartNew(InnerMethod, TaskCreationOptions.AttachedToParent);
            WriteLine("Finishing outer method");
        }
        static void InnerMethod()
        {
            WriteLine("Starting Inner method");
            Thread.Sleep(2000);
            WriteLine("Finishing inner method");
        }

        static void Main(string[] args)
        {
            var outer = Task.Factory.StartNew(OuterMethod);
            outer.Wait();
            WriteLine("Console App is stopping");
        }
    }
}
