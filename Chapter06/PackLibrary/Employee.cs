using System;
using static System.Console;

namespace Pack.Shared
{
    public class Employee : Person
    {
        public string EmployeeCode{get; set;}
        public DateTime HireDate{get; set;}

        public new void WriteToConsole()
        {
            WriteLine($"{Name} with employee code {EmployeeCode} was hired on {HireDate:dd/MM/yy} ");
        }

        public override string ToString()
        {
            return $"{Name}'s code is {EmployeeCode}";
        }
    }
}