﻿using System;
using static System.Console;
using System.Text;

namespace WorkingWithEncodings
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Encodings:");
            WriteLine("[1] ASCII");
            WriteLine("[2] UTF-7");
            WriteLine("[3] UTF-8");
            WriteLine("[4] UTF-16");
            WriteLine("[5] UTF-32");
            WriteLine("[any other key] Default");

            Write("Press a Number to choose an Encoding: ");
            ConsoleKey number = ReadKey(intercept: false).Key;

            WriteLine();
            WriteLine();

            Encoding encoder = number switch
            {
                ConsoleKey.D1 => Encoding.ASCII,
                ConsoleKey.D2 => Encoding.UTF7,
                ConsoleKey.D3 => Encoding.UTF8,
                ConsoleKey.D4 => Encoding.Unicode,
                ConsoleKey.D5 => Encoding.UTF32,
                _ => Encoding.Default
            };

            string message = "A pint of milk is $1.23";

            byte[] encoded = encoder.GetBytes(message);

            WriteLine("{0} uses {1:N0} bytes.",encoder.GetType().Name, encoded.Length);

            WriteLine("BYTE  HEX  CHAR");
            foreach (var item in encoded)
            {
                WriteLine($"{item,4} {item.ToString("X"),4} {(char)item,5}");
            }

            string decoded = encoder.GetString(encoded);//Note that it isn't to string
            WriteLine(decoded);
        }
    }
}
