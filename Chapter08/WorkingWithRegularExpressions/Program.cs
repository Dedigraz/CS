using System;
using System.Text.RegularExpressions;
using static System.Console;

namespace WorkingWithRegularExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            // Write("Enter your age: ");
            // string age = ReadLine();

            // var ageChecker = new Regex(@"^\d+$");

            // if(ageChecker.IsMatch(age))
            // {
            //     WriteLine("Thank you");
            // }
            // else{
            //     WriteLine($"This is not a valid age: {age}");
            // }

            string films = "\"Monsters, Inc.\",\"I, Tonya\",\"Lock, Stock and Two Smoking Barrels\"";
            var dumb = films.Split(",");

            WriteLine("Dumb attempt at splitting: ");
            foreach (var item in dumb)
            {
                WriteLine(item);
            }

            var csv = new Regex("(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)");

            MatchCollection filmSmart = csv.Matches(films);

            foreach (Match film in filmSmart)
            {
                WriteLine(film.Groups[2].Value);
            }
        }
    }
}
