using System;

namespace Pack.Shared
{
    public class Thing
    {
        public object Data = default (object);

        public string Process(object input)
        {
            if (input == Data)
            {
                return $"{input} and {Data} are the same";
            }
            else{
                return $"{input} and {Data} are not the same";
            }
        }
    }
}