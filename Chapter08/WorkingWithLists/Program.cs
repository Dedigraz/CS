using System;
using System.Collections.Generic;
using static System.Console;
using System.Collections.Immutable;

namespace WorkingWithLists
{
    class Program
    {
        static void Main(string[] args)
        {
            var cities = new List<string>{};
            cities.Add("London");
            cities.Add("Paris");
            cities.Add("Milan");

            WriteLine("Initial List: ");
            foreach (var item in cities)
            {
                WriteLine(item);
            }

            WriteLine($"The first city is {cities[0]}");
            WriteLine($"The Last city is {cities[cities.Count -1]}");

            cities.Insert(0, "Sydney");
            WriteLine("After inserting Sydney at index 0");
            foreach (string city in cities)
            {
                WriteLine($" {city}");
            }

            cities.RemoveAt(1);
            cities.Remove("Milan");

            WriteLine("After removing two cities");
            foreach (var item in cities)
            {
                WriteLine(item);
            }


            var immutableCities = cities.ToImmutableList();
            var newList = immutableCities.Add("Rio");
            Write("Immutable list of cities:");
            foreach (string city in immutableCities)
            {
                Write($" {city}");
            }
            WriteLine();
            Write("New list of cities:");
            foreach (string city in newList)
            {
                Write($" {city}");
            }
            WriteLine();
        }
    }
}
