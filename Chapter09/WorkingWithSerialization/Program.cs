using System;
using System.Collections.Generic; // List<T>, HashSet<T>
using System.Xml.Serialization; // XmlSerializer
using System.IO; // FileStream
using static System.Console;
using static System.Environment;
using static System.IO.Path;
using System.Threading.Tasks;
using NuJson = System.Text.Json.JsonSerializer;

namespace WorkingWithSerialization
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var people = new List<Person>
            {
                new Person(30000M) { FirstName = "Alice",
                LastName = "Smith",
                DateOfBirth = new DateTime(1974, 3, 14) },

                new Person(40000M) { FirstName = "Bob",
                LastName = "Jones",
                DateOfBirth = new DateTime(1969, 11, 23) },
                
                new Person(20000M) { FirstName = "Charlie",
                LastName = "Cox",
                DateOfBirth = new DateTime(1984, 5, 4),
                Children = new HashSet<Person>
                { new Person(0M) { FirstName = "Sally",
                LastName = "Cox",
                DateOfBirth = new DateTime(2000, 7, 12) } } }
            };

            var xs = new XmlSerializer(typeof(List<Person>));

            string path = Combine(CurrentDirectory, "people.xml");

            using (FileStream stream = File.Create(path))
            {
                xs.Serialize(stream,people);
            }

            WriteLine("Written {0:N0} bytes of XML to {1}",
                arg0: new FileInfo(path).Length,
                arg1: path);
            WriteLine();
            
            WriteLine(File.ReadAllText(path));

            using (FileStream xmLoad = File.Open(path, FileMode.Open))
            {
                var loadedPeople = (List<Person>)xs.Deserialize(xmLoad);
                foreach (var item in loadedPeople)
                {
                    WriteLine($"{item.LastName} has {item.Children.Count} children");
                }
            }

            string jsonPath = Combine(CurrentDirectory, "people.json");
            using (StreamWriter jsonStream = File.CreateText(jsonPath))
            {
                var jss = new Newtonsoft.Json.JsonSerializer();

                jss.Serialize(jsonStream,people);
            }
            WriteLine();
            
            WriteLine("Written {0:N0} bytes of JSON to: {1}",
            arg0: new FileInfo(jsonPath).Length,
            arg1: jsonPath);
            // Display the serialized object graph
            
            WriteLine(File.ReadAllText(jsonPath));

            using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
            {
                var loadedPeople = (List<Person>) await NuJson.DeserializeAsync(utf8Json: jsonLoad,returnType: typeof(List<Person>));

                foreach (var item in loadedPeople)
                {
                    WriteLine($"{item.LastName} has {item.Children?.Count}");
                }

            }
        }
    }
}
