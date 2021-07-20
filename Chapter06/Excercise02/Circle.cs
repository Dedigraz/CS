using System;
namespace Excercise02
{
    public class Circle : Shape
    {
        public Circle(double r)
        {
            Height = (decimal)r;
            Width = (decimal)r;
            Area = (decimal)(Math.PI * r*r);
        }
    }
}