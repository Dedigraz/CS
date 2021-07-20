using System;
using static System.Console;
using System.Collections.Generic;

namespace Packt.Shared
{
    public partial class Person: Object
    {
        public string Name;
        public DateTime DateOfBirth;
        public WondersOfTheAncientWorld FavouriteAncientWonder;
        public WondersOfTheAncientWorld BucketList;
        public List<Person> Children =  new List<Person>();
        public const string Species = "Homosapien";
        public readonly string HomePlanet = "Earth";
        public readonly DateTime Instantiated;

        public Person()
        {
            Name = "Unknown";
            Instantiated = DateTime.Now;
        }
        public Person(string initialName, string planet)
        {
            Name = initialName;
            HomePlanet = planet;
            Instantiated = DateTime.Now;
        }

        public void WriteToConsole()
        {
            WriteLine($"{Name} was born on {DateOfBirth:dddd}");
        }
        public string GetOrigin()
        {
            return $"{Name} was born on {HomePlanet}";
        }
        public (string Name, int Number) GetFruit()
        {
            return("Apples",5);
        }

        public string SayHello(){
            return $"{Name} says hello";
        }
        public string SayHello(string name)
        {
            return $"{Name} says hello to {name}";
        }
        public string OptionalParameters(
            string command ="run",
            double number = 0.0,
            bool active = true)
        {
            return $"commmand is {command}, number is {number} and active is {active}";
        }

        public void PassingParameters(int x,ref int y, out int z )
        {
            z=99;

            x++;
            y++;
            z++;
        }
    }
}
