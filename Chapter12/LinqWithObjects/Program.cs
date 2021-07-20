using System;
using System.Linq;
using static System.Console;
namespace LinqWithObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqWithArrayOfExceptions();
            //LinqWithArrayOfStrings();
        }

        static void LinqWithArrayOfStrings()
        {
            var names = new string[] {"Michael", "Pam", "Jim", "Dwight",
                "Angela", "Kevin", "Toby", "Creed"};
            
            var query = names
                .Where(names => names.Length > 4)
                .OrderBy(names => names.Length)
                .ThenBy(names => names);

            //var query = names.Where(NameLongerThan4);
            //var query = names.Where(new Func<string, bool>(NameLongerThan4));

            foreach (string item in query)
            {
                WriteLine(item);
            }
        }
        static bool NameLongerThan4(string name)
        {
            return name.Length > 4;
        }

        static void LinqWithArrayOfExceptions()
        {
            var errors = new Exception[]
            {
                new ArgumentException(),
                new SystemException(),
                new IndexOutOfRangeException(),
                new InvalidCastException(),
                new InvalidOperationException(),
                new NullReferenceException(),
                new DivideByZeroException(),
                new OverflowException(),
                new ApplicationException()
            };

            var art = errors.OfType<ArithmeticException>();

            foreach (var item in art)
            {
                WriteLine(item);
            }
        }

    }
}
