using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Utilities
{
    class InputHelper
    {
        public static string ReadNonEmptyString(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("❌ Input cannot be empty. Try again.");
                }
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        public static decimal ReadDecimal(string prompt)
        {
            decimal value;
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!decimal.TryParse(input, out value) || value <= 0)
                {
                    Console.WriteLine("❌ Enter a valid positive number.");
                }
                else
                {
                    return value;
                }
            } while (true);
        }

        public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int value;
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!int.TryParse(input, out value) || value < min || value > max)
                {
                    Console.WriteLine($"❌ Please enter a number between {min} and {max}.");
                }
                else
                {
                    return value;
                }
            } while (true);
        }

        public static string ReadAccountNumber()
        {
            string input;
            do
            {
                Console.Write("Enter 6-digit account number: ");
                input = Console.ReadLine()?.Trim();
                if (input.Length != 6 || !input.All(char.IsDigit))
                {
                    Console.WriteLine("❌ Account number must be exactly 6 digits.");
                }
                else
                {
                    return input;
                }
            } while (true);
        }
    }
}
