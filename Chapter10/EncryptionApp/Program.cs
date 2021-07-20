using System;
using CryptographyLib;
using static System.Console;
using System.Security.Cryptography;

namespace EncryptionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter a message you want to encrypt: ");
            string message = ReadLine();

            Write("Passsword: ");
            string password = ReadLine();

            string cryptoText = Protector.Encrypt(message, password);
            WriteLine($"Encrypted text: {cryptoText}");
            Write("Enter the password: ");
            string password2 = ReadLine();

            try
            {
                string clearText = Protector.Decrypt(cryptoText, password);
                WriteLine($"Decryptrd Text: {clearText}");
            }
            catch (CryptographicException ex)
            {
                WriteLine($"You entered the wrong Password \n More details: {ex.Message}");
            }
            catch(Exception ex)
            {
                WriteLine($"Non crytographic exception: {ex.GetType().Name}, {ex.Message}");
            }


        }
    }
}
