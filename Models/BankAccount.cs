using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models
{
    public class BankAccount
    {
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; private set; }
        
        public BankAccount()
        {
            Transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                return;
            }

            Balance += amount;
            Transactions.Add(new Transaction("Deposit", amount, Balance));
        }
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive.");
                return false;
            }

            if (amount > Balance)
            {
                Console.WriteLine("Insufficient funds.");
                return false;
            }

            Balance -= amount;
            Transactions.Add(new Transaction("Withdraw", amount, Balance));
            return true;
        }

    }
}
