namespace Excercise02
{
    public class Square : Shape
    {
        public Square(decimal h)
        {
            Height = h;
            Width = h;

            Area = Height * Width;
        }
    }
}