using System;
using System.Reflection;
using static System.Console;
using System.Linq;

namespace WorkingWithReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Assembley Metadata: ");
            Assembly assembly = Assembly.GetEntryAssembly();
            WriteLine($"{assembly.FullName}");
            WriteLine($"{assembly.Location}");

            var attributes = assembly.GetCustomAttributes();
            WriteLine("Attributes: ");
            foreach (Attribute a in attributes)
            {
                WriteLine($"{a.GetType()}");
            }

            var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            WriteLine($"version = {version.InformationalVersion}");
            var company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            WriteLine($"Company: {company.Company}");

            WriteLine();
            WriteLine($"*Types: ");
            Type[] types = assembly.GetTypes();
            foreach (var type in types)
            {
                WriteLine();
                WriteLine($"Type name: {type.Name}");
                MemberInfo[] member = type.GetMembers();
                foreach(MemberInfo m in member)
                {
                    WriteLine($"Type: {m.MemberType}, Name:{m.Name} ({m.DeclaringType.Name})");

                    var coders = m.GetCustomAttributes<CoderAttribute>().OrderByDescending(c => c.LastModified);

                    foreach (CoderAttribute coder in coders)
                    {
                        WriteLine("-> Modified by {0} on {1}", coder.Coder, coder.LastModified.ToShortDateString());
                    }
                }
                
            }
        }

        [Coder("Mark Price", "22 August 2019")]
        [Coder("Johnni Rasmussen", "13 September 2019")]
        public static void DoStuff()
        {

        }
    }
}
