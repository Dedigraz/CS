using static System.Console;

namespace Pack.Shared
{
    public class DvdPlayer : IPlayable
    {
        public void Pause()
        {
            WriteLine($"Dvd player is pausing.");
        }

        public void Play()
        {
            WriteLine("Dvd player is playing");
        }
    }
}