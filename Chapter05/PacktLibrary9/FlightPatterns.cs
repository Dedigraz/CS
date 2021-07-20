namespace Packt.Shared
{
    public class BuisnessClassPassenger
    {
        public override string ToString()
        {
            return $"Buisness Class";
        } 
    }

    public class FirstClassPassenger
    {
        public int AirMiles{get; set;}
        public override string ToString()
        {
            return $"First class with {AirMiles} air miles on ";
        }
    }

    public class CoachClassPassenger
    {
        public double CarryOnKG{get; set;}
        public override string ToString()
        {
            return $"Coach passenger with carry on weighing{CarryOnKG:N2}kg ";
        }
    }
}