using System;

namespace Excercise02
{
    public class Rectangle : Shape
    {
        // public override decimal Area()
        // {
        //     return base.Area();
        // }
        
        public Rectangle(double height ,double width )
        {
            Height = (decimal)height;
            Width = (decimal)width;

            Area = Height * Width;
        }
    }
}