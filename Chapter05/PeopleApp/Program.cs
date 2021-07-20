using System;
using static System.Console;
using Packt.Shared;

namespace PeopleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bob = new Person();
            bob.Name = "Bob Smith";
            bob.DateOfBirth = new DateTime(1969,12,11);

            WriteLine($"{bob.Name} was born on {bob.DateOfBirth:dddd, d MMMM yyyy}");

            var alice = new Person{
                Name = "Alice Jones",
                DateOfBirth = new DateTime(2002,5,28)
            };

            WriteLine($"{alice.Name} was born on {alice.DateOfBirth: dd MMM yy}");

            bob.FavouriteAncientWonder = WondersOfTheAncientWorld.ColossusOfRhodes;
            WriteLine($"{bob.Name}'s favourite ancient wonder is the {bob.FavouriteAncientWonder} and it's indexed at {(int)bob.FavouriteAncientWonder}");

            bob.BucketList = WondersOfTheAncientWorld.GreatPyramidsOfGiza
                             | WondersOfTheAncientWorld.HangingGardensOfBabylon;
            WriteLine($"{bob.Name}'s bucket list is {bob.BucketList}");

            bob.Children.Add(new Person { Name = "Alfred" });
            bob.Children.Add(new Person { Name = "Zoe" });
            WriteLine(
            $"{bob.Name} has {bob.Children.Count} children:");
            for (int child = 0; child < bob.Children.Count; child++)
            {
                WriteLine($" {bob.Children[child].Name}");
            }

            BankAccount.IntrestRate = 0.22M;

            var JonesAccount = new BankAccount{
                AccountName = "Mrs. Jones",
                Balance = 2400
            };

            WriteLine($"{JonesAccount.AccountName} earns {JonesAccount.Balance *BankAccount.IntrestRate:C}");


            var gerrierAccount = new BankAccount
            {
                AccountName = "Ms. Gerrier",
                Balance = 98
            };

            WriteLine($"{gerrierAccount.AccountName} earns {gerrierAccount.Balance * BankAccount.IntrestRate:C}");

            WriteLine($"{bob.Name} is a {Person.Species}");
            WriteLine($"{bob.Name} was born on {bob.HomePlanet}");

            var blankPerson = new Person();
            WriteLine(format:
            "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
            arg0: blankPerson.Name,
            arg1: blankPerson.HomePlanet,
            arg2: blankPerson.Instantiated);

            var gunny = new Person("Gunny","mars");
            WriteLine(format:"{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
                arg0: gunny.Name,
                arg1:gunny.HomePlanet,
                gunny.Instantiated
                );

            bob.WriteToConsole();
            WriteLine(bob.GetOrigin());

            (string Name, int Number) fruit = bob.GetFruit();
            WriteLine($" there are {fruit.Number}, {fruit.Name}");

            var thing1 = ("neville",4);
            WriteLine($"{thing1.Item1} has {thing1.Item2} children.");

            var thing2 = (bob.Name, bob.Children.Count);
            WriteLine($"{thing2.Name} has {thing2.Count} children.");

            (string fruitName, int fruitNumber) = bob.GetFruit();
            WriteLine($"{fruitName} {fruitNumber}");

            WriteLine(bob.SayHello());
            WriteLine(bob.SayHello("Emily"));


            WriteLine(bob.OptionalParameters());
            WriteLine(bob.OptionalParameters("Jump!", 98.5));
            //with named parameter's you can change the order in which you call them
            WriteLine(bob.OptionalParameters(number: 52.7, command: "Hide!"));

            int a = 10;
            int b = 20;
            int c = 30;

            WriteLine($"Before: a:{a}, b:{b}, c:{c}");

            bob.PassingParameters(a,ref b,out c);

            WriteLine($"After: a:{a}, b:{b}, c:{c}");

            int d = 10;
            int e = 20;
            WriteLine(
            $"Before: d = {d}, e = {e}, f doesn't exist yet!");
            // simplified C# 7.0 syntax for the out parameter
            bob.PassingParameters(d, ref e, out int f);
            WriteLine($"After: d = {d}, e = {e}, f = {f}");

            var sam = new Person{
                Name = "Sam",
                DateOfBirth = new DateTime(1927,1,27)
            };

            WriteLine(sam.Origin);
            WriteLine(sam.Greeting);
            WriteLine(sam.Age);

            sam.FavoriteIceCream = "Chocolate Fudge";
            WriteLine($"Sam's favorite ice-cream flavor is {sam.FavoriteIceCream}.");
            sam.FavoritePrimaryColor = "Red";
            WriteLine($"Sam's favorite primary color is {sam.FavoritePrimaryColor}.");

            sam.Children.Add(new Person { Name = "Charlie" });
            sam.Children.Add(new Person { Name = "Ella" });
            WriteLine($"Sam's first child is {sam.Children[0].Name}");
            WriteLine($"Sam's second child is {sam.Children[1].Name}");
            WriteLine($"Sam's first child is {sam[0].Name}");
            WriteLine($"Sam's second child is {sam[1].Name}");
        
        
            object[] passengers = 
            {
                new FirstClassPassenger{AirMiles = 1_419},
                new FirstClassPassenger{AirMiles = 16_562},
                new BuisnessClassPassenger(),
                new CoachClassPassenger{CarryOnKG = 25.7},
                new CoachClassPassenger{CarryOnKG = 0}
            };

            foreach (object passenger in passengers)
            {
                decimal flightCost = passenger switch
                {
                /*C# 8 Syntax
                    FirstClassPassenger p when p.AirMiles > 35000 => 1500M,
                    FirstClassPassenger p when p.AirMiles > 15000 => 1750M,
                    FirstClassPassenger _   => 2000M,
                    BuisnessClassPassenger _ => 1000M,
                    CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
                    CoachClassPassenger _ => 650M,
                    _ => 800M */

                    FirstClassPassenger p => p.AirMiles switch
                    {
                        > 35000 => 1500M,
                        > 15000 => 1750M,
                        _    => 2000M
                    },

                    BuisnessClassPassenger  => 1000M,
                    CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
                    CoachClassPassenger   => 650M,
                    

                    _ => 800M

                };

                WriteLine($"Flight cost {flightCost:C} for passenger");
            }

            var jeff = new ImmutablePerson
            {
                FirstName = "Jeff",
                LastName = "Winger"
            };

            // jeff.FirstName = "Geoff";

            var car = new ImmutableVehicle
            {
                Brand = "Mazda MX - 5 RF",
                Colour ="Soul Red Crystal Metallic",
                Wheels = 4
            };

            var repaintedCar = car with {Colour = "Polymetal Grey Mettalic"};

            WriteLine($"Original colour was {car.Colour}, new colour is {repaintedCar.Colour}");

            var oscar = new ImmutableAnimal("Oscar", "Labrador");
            var (who, what) = oscar;
            WriteLine($"{who} is a {what}");
        }
    }
}
