using System;
using static System.Console;

namespace WorkingWithText
{
    class Program
    {
        static void Main(string[] args)
        {
            string city = "Abuja";
            WriteLine($"{city} is {city.Length} characters long");
            WriteLine($"First char is {city[0]} and third is {city[2]}.");

            string cities = "Paris,Berlin,Madrid,NewYork";
            string[] citiesArray = cities.Split(",");

            foreach (var item in citiesArray)
            {
                WriteLine(item);
            }

            string fullname = "Alan Jones";
            var indexof = fullname.IndexOf(" ");

            var firstname = fullname.Substring(0, indexof);
            var lastname = fullname.Substring(indexof + 1);

            WriteLine($"{firstname} son of {lastname}");

            string company ="Microsoft";
            bool startsWithM = company.StartsWith('M');
            bool containsN = company.Contains('N');
            WriteLine($"Starts with M: {startsWithM} ContainsN: {containsN}");

            string trip = string.Join(" => ",citiesArray);
            WriteLine(trip);

            string fruit = "Apple";
            int price = 43;
            DateTime when = new DateTime(2012,12,13);
            WriteLine($"{fruit} cost {price:C} on {when:dddd}.");
            WriteLine(string.Format("{0} cost {1:C} on {2:dddd}.",fruit, price, when));
        }
    }
}
