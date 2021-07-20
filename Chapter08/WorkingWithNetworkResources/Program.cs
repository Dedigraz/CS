using System;
using System.Net;
using static System.Console;
using System.Net.NetworkInformation;

namespace WorkingWithNetworkResources
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a valid web address: ");
            string url = ReadLine();
            if (string.IsNullOrWhiteSpace(url))
            {
                url = "https://world.episerver.com/cms/?q=pagetype";
            }

            var uri = new Uri(url);
            WriteLine($"The web address is at {url}");
            WriteLine($"Port: {uri.Port}");
            WriteLine($"Scheme: {uri.Scheme}");
            WriteLine($"Host: {uri.Host}");
            WriteLine($"Absolute Path: {uri.AbsolutePath}");
            WriteLine($"Query: {uri.Query}");

            IPHostEntry hostEntry = Dns.GetHostEntry(uri.Host);
            WriteLine($"{hostEntry.HostName} has the following ip adresses:");
            foreach (IPAddress address in hostEntry.AddressList)
            {
                WriteLine($"{address}");
            }


            try
            {
                var ping = new Ping();
                WriteLine("Pinging server. Please wait...");
                PingReply reply = ping.Send(uri.Host);
                WriteLine($"{uri.Host} was pinged and replied: {reply.Status}.");
                if (reply.Status == IPStatus.Success)
                    {
                        WriteLine("Reply from {0} took {1:N0}ms",
                        reply.Address, reply.RoundtripTime);
                    }
            }
            catch (Exception ex)
            {
                WriteLine($"{ex.GetType().ToString()} says {ex.Message}");
            }
        }
    }
}
