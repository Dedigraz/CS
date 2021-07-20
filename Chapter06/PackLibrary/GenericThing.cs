using System;
namespace Pack.Shared
{
    public class GenericThing<T> where T: IComparable
    {
        public T Data = default(T);
        public string Process(T input)
        {
            if(Data.CompareTo(input) == 0)
            {
                return "Input and data are the same";
            }
            else{
                return "input and data are not the same";
            }
        }
    }
}