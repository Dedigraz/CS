using System;
using System.Text.RegularExpressions;
using static System.Console;

namespace Excercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The default regular expression checks for at least one digit.");
            System.Console.Write("Enter a regular expression (or press ENTER to use the default):");
            string ans = ReadLine();
            Regex regex;
            if(string.IsNullOrWhiteSpace(ans))
            {
                regex = new Regex(@"^\d+$");
            }
            else
            {
                regex = new Regex(ans);
            }
            Write("Enter some input: ");
            string input =ReadLine();
            WriteLine($"{input} matches {ans}: {regex.IsMatch(input)}");

            WriteLine("Press ESC to end or any key to try again.");
            ConsoleKeyInfo exit = ReadKey();
            // if (exit.Key == ConsoleKey.Escape)
            // {
            //     Console.Clear();
            // }
            // else
            // {
            //     return;
            // }

        }
    }
}
