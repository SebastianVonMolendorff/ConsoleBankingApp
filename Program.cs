using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Services;

namespace BankingApp
{
    class Program
    {
        static void Main(string[] args)
            {
                var bank = new BankService();
                int choice;

                do
                {
                    Console.WriteLine("\n==== Console Banking App ====");
                    Console.WriteLine("[1] Create New Account");
                    Console.WriteLine("[2] Login to Existing Account");
                    Console.WriteLine("[0] Exit");
                    Console.Write("Select an option: ");

                    string input = Console.ReadLine();
                    if (!int.TryParse(input, out choice))
                    {
                        Console.WriteLine("❌ Please enter a valid number.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            bank.CreateAccount();
                            break;

                        case 2:
                            var account = bank.Login();
                            if (account != null)

                            {
                                bank.ShowAccountMenu(account);
                            }
                            break;

                        case 0:
                            Console.WriteLine("Thank you for using the Banking App!");
                            break;

                        default:
                            Console.WriteLine("❌ Invalid selection.");
                            break;
                    }

                } while (choice != 0);

            }
    }
}
