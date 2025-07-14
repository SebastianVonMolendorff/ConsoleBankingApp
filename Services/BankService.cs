using BankingApp.Data;
using BankingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Utilities;

namespace BankingApp.Services
{
    class BankService
    {
        private List<BankAccount> _accounts;

        public BankService()
        {
            _accounts = DataStore.LoadAccounts();
        }

        public void CreateAccount()
        {
            string name = InputHelper.ReadNonEmptyString("Enter your name: ");
            string password = InputHelper.ReadNonEmptyString("Choose a password: ");

            string accNumber = GenerateUniqueAccountNumber();
            var account = new BankAccount
            {
                AccountHolder = name,
                Password = password,
                AccountNumber = accNumber
            };

            _accounts.Add(account);
            DataStore.SaveAccounts(_accounts);
            Console.WriteLine($"\n✅ Account created! Your account number is: {accNumber}");
        }

        public BankAccount Login()
        {
            string accNumber = InputHelper.ReadAccountNumber();
            string password = InputHelper.ReadNonEmptyString("Enter password: ");

            var account = _accounts.FirstOrDefault(a =>
                a.AccountNumber == accNumber && a.Password == password);

            if (account != null)
            {
                Console.WriteLine("\n✅ Login successful!");
                return account;
            }

            Console.WriteLine("❌ Invalid credentials.");
            return null;
        }

        public void ShowAccountMenu(BankAccount account)
        {
            int choice;
            do
            {
                Console.WriteLine($"\n==== Welcome, {account.AccountHolder} ====");
                Console.WriteLine("[1] View Balance");
                Console.WriteLine("[2] Deposit");
                Console.WriteLine("[3] Withdraw");
                Console.WriteLine("[4] View Transactions");
                Console.WriteLine("[5] Transfer To Another Account");
                Console.WriteLine("[0] Logout");

                choice = InputHelper.ReadInt("Choose an option: ", 0, 5);

                switch (choice)
                {
                    case 1:
                        Console.WriteLine($"Current Balance: R{account.Balance}");
                        break;

                    case 2:
                        decimal depositAmount = InputHelper.ReadDecimal("Enter amount to deposit: ");
                            account.Deposit(depositAmount);
                            DataStore.SaveAccounts(_accounts);
                        break;

                    case 3:
                        decimal withdrawAmount = InputHelper.ReadDecimal("Enter amount to withdraw: ");
                            account.Withdraw(withdrawAmount);
                            DataStore.SaveAccounts(_accounts);
                        break;

                    case 4:
                        if (account.Transactions.Count == 0)
                        {
                            Console.WriteLine("No transactions available.");
                        }
                        else
                        {
                            Console.WriteLine("📄 Transaction History:");
                            foreach (var t in account.Transactions)
                            {
                                Console.WriteLine($"{t.Date:dd/MM/yyyy HH:mm} | {t.Type} | R{t.Amount} | Balance: R{t.BalanceAfter}");
                            }
                        }
                        break;

                    case 5:
                        TransferBetweenAccounts(account);
                        break;

                    case 0:
                        Console.WriteLine("Logging out...");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

            } while (choice != 0);
        }

        public void TransferBetweenAccounts(BankAccount sender)
        {
            string recipientAccNumber = InputHelper.ReadAccountNumber();
            if (recipientAccNumber == sender.AccountNumber)
            {
                Console.WriteLine("❌ Cannot transfer to the same account.");
                return;
            }

            var recipient = _accounts.FirstOrDefault(a => a.AccountNumber == recipientAccNumber);

            if (recipient == null)
            {
                Console.WriteLine("❌ Recipient account not found.");
                return;
            }

            decimal amount = InputHelper.ReadDecimal("Enter amount to transfer: ");

            if (sender.Balance < amount)
            {
                Console.WriteLine("❌ Insufficient funds.");
                return;
            }

            sender.Balance -= amount;
            recipient.Balance += amount;

            sender.Transactions.Add(new Transaction("Transfer", amount, sender.Balance, $"To: {recipient.AccountNumber}"));
            recipient.Transactions.Add(new Transaction("Transfer", amount, recipient.Balance, $"From: {sender.AccountNumber}"));

            DataStore.SaveAccounts(_accounts);

            Console.WriteLine($"✅ Successfully transferred R{amount} to account {recipient.AccountNumber}.");
        }

        private string GenerateUniqueAccountNumber()
        {
            string accNumber;
            Random rand = new Random();
            do
            {
                accNumber = rand.Next(100000, 999999).ToString();
            } while (_accounts.Any(a => a.AccountNumber == accNumber));
            return accNumber;
        }


    }
}
