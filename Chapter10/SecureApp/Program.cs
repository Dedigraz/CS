using System;
using static System.Console;
using CryptographyLib;
using System.Threading;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Security.Claims;

namespace SecureApp
{
    class Program
    {
        static void SecureFeature()
        {
            if(Thread.CurrentPrincipal == null)
            {
                WriteLine("You have to be logged in to use this feature");
            }
            if(!Thread.CurrentPrincipal.IsInRole("Admin"))
            {
                WriteLine("User must be an admin to use this feature");
            }
            else
            {
                WriteLine("You are Logged in");
            }
        }
        static void Main(string[] args)
        {
            Protector.Register("alice","Pa$$word",new[] {"Admin"});
            Protector.Register("bob", "Passw0rd",new[]{"sales","teamLeads"});
            Protector.Register("Eve", "Pa$$w0rd");
            
            Write($"Enter your user name: ");
            string username = ReadLine();
            Write($"Enter your password: ");
            string password = ReadLine();
            
            Protector.Login(username, password);
            if (Thread.CurrentPrincipal == null)
            {
                WriteLine("Log in failed.");
                return;
            }
            
            
            var p = Thread.CurrentPrincipal;
            
            WriteLine($"IsAuthenticated: {p.Identity.IsAuthenticated}");
            WriteLine($"AuthenticationType: {p.Identity.AuthenticationType}");
            
            WriteLine($"Name: {p.Identity.Name}");
            WriteLine($"IsInRole(\"Admins\"): {p.IsInRole("Admins")}");
            WriteLine($"IsInRole(\"Sales\"): {p.IsInRole("Sales")}");
            
            if (p is ClaimsPrincipal)
            {
                WriteLine(
                $"{p.Identity.Name} has the following claims:");
                foreach ( var Claim in (p as ClaimsPrincipal).Claims)
                {
                    WriteLine($"{Claim.Type}: {Claim.Value}");
                }
            }
            try
            {
                SecureFeature();
            }
            catch (Exception ex)
            {
                WriteLine($"{ex.GetType()}, {ex.Message}");
                throw;
            }
        }
    }
}
