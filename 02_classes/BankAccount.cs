using System;
using System.Collections.Generic;

namespace _02_classes
{
    public class BankAccount
    {
        //these are properties
        public string Number { get; }
        public string Owner { get; set; }

        private List<Transaction> allTransactions = new List<Transaction>();
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                    balance += item.Amount;
                return balance;
            } 
        }

        private static int accountNumberSeed = 1234567890;

        //methods that create deposit and withdrawal
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            if (Balance - amount < 0)
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }

        //method that creates transaction history
        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            report.AppendLine("Date\t\tAmount\tNote");
            foreach (var item in allTransactions)
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{item.Notes}");

            return report.ToString();
        }

        //BankAccount constructor
        public BankAccount(string name, decimal initialBalance)
        {
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;
            
            this.Owner = name;
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }
    }
}