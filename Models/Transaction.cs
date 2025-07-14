using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAfter { get; set; }
        public string Note { get; set; }
        public Transaction(string type, decimal amount, decimal balanceAfter, string note)
        {
            Date = DateTime.Now;
            Type = type;
            Amount = amount;
            BalanceAfter = balanceAfter;
            Note = note;
        }

        public Transaction(string v, decimal amount, decimal balance)
        {
            Amount = amount;
        }
    }
}
