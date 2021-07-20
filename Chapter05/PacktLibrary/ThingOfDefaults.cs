using System;
using System.Collections.Generic;

namespace Packt.Shared
{
    public class ThingOfDefaults
    {
        public int Population;
        public DateTime When; 
        public string Name;
        public List<Person> People;

        public ThingOfDefaults()
        {
            #region Old Csharp way of Initializing Defaults 
                // Population = default(int);
                // When = default(DateTime);
                // Name = default(string);
                // People = default(List<Person>);
            #endregion
            Population = default;
            When = default;
            Name = default;
            People = default;
        }
    }
}