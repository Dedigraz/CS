using static System.Console;
namespace Pack.Shared
{
    public interface IPlayable
    {
         void Play();
         void Pause();

         void Stop()
         {
             WriteLine("Default implementation os stop");
         }
    }
}