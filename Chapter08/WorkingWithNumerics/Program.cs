using System;
using static System.Console;
using System.Numerics;
namespace WorkingWithNumerics
{
    class Program
    {
        static void Main(string[] args)
        {
            var longest = ulong.MaxValue;

            WriteLine($"{longest,40:N0}");

            var atomsInTheUniverse = BigInteger.Parse("123456789012345678901234567890");
            WriteLine($"{atomsInTheUniverse,40:N0}");

            var c1 = new Complex(4,2);
            var c2 = new Complex(2,3);

            var c3 = c1 + c2;
            WriteLine($"{c1} + {c2} is = {c3}");
        }
    }
}
