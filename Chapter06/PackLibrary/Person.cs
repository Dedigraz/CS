using System;
using System.Collections.Generic;
using static System.Console;

namespace Pack.Shared
{
    public class Person : IComparable<Person>
    {
        public string Name;
        public DateTime DateOfBirth;
        public List<Person> Children = new List<Person>();

        public void WriteToConsole()
        {
            WriteLine($"{Name} was born on a {DateOfBirth:dddd}");
        }

        public static Person Procreate(Person a, Person b)
        {
            var baby = new Person
            {
                Name = $"The baby of {a.Name} and {b.Name}"
            };
            a.Children.Add(baby);
            b.Children.Add(baby);

            return baby;
        }

        public Person ProcreateWith(Person b)
        {
            return Procreate(this,b);
        }

        public static Person operator * (Person a, Person b)
        {
            return Procreate(a,b);
        }

        public static int Factorial(int number)
        {
            if (number<0)
            {
                throw new ArgumentException($"{nameof(number)} is not a valid argument");
            }
            return localFactorial(number);

            int localFactorial(int localNumber)
            {
                if(localNumber < 1) return 1;
                else return localNumber * localFactorial(localNumber -1);
            }
        }

        public event EventHandler Shout;
        public int AngerLevel;
        public void Poke()
        {
            AngerLevel++;
            if(AngerLevel >= 3)
            {
                //C# also allows the following method to be simplified into
                //Shout?.Invoke(this,EventArgs.Empty)
                if (Shout != null)
                {
                    Shout(this, EventArgs.Empty);
                }
            }

        }

        public override string ToString()
        {
            return $"{Name} is a {base.ToString()}";
        }

        public int CompareTo(Person other)
        {
            return Name.CompareTo(other.Name);
        }

        public void TimeTravel(DateTime when)
        {
            if (when <= DateOfBirth)
            {
                throw new PersonException("If you travel back in time to a date earlier than your own birth, then the universe will explode!");
            }
            else
            {
                WriteLine($"Welcome to {when:yyyy}!");
            }
        }
    }

    public class PersonComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            int result = x.Name.Length.CompareTo(y.Name.Length);

            if (result == 0)
            {
                return x.Name.CompareTo(y.Name);
            }
            else{
                return result;
            }
        }
    }
}
