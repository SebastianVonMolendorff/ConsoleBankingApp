using BankingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace BankingApp.Data
{
    public static class DataStore
    {
        private static readonly string FilePath = "accounts.json";

        public static void SaveAccounts(List<BankAccount> accounts)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(accounts, options);
            File.WriteAllText(FilePath, json);
        }

        public static List<BankAccount> LoadAccounts()
        {
            if (!File.Exists(FilePath))
                return new List<BankAccount>();

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<BankAccount>>(json);
        }
    }
}
