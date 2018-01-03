using System;
using System.Collections.Generic;

//Introduction to classes
//https://docs.microsoft.com/en-us/dotnet/csharp/quick-starts/introduction-to-classes

namespace _02_classes
{
    class Program
    {
        static void Main(string[] args)
        {
            //clear screen
            Console.Clear();

            //open a new account
            var account = new BankAccount("John", 1000);
            Console.WriteLine();
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance.");
            Console.WriteLine();
            
            //make withdrawal
            account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
            Console.WriteLine(account.Balance);
            Console.WriteLine();

            //make deposit
            account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
            Console.WriteLine(account.Balance);
            Console.WriteLine();

            //test that the initial balance must be positive
            try
            {
                var invalidAccount = new BankAccount("invalid", -55);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Exception caught creating account with negative balance.");
                Console.WriteLine(e.ToString());
            }

            //test for a negative balance
            try
            {
                account.MakeWithdrawal(750, DateTime.Now, "Attempt to overdraw");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Exception caught trying to overdraw");
                Console.WriteLine(e.ToString());
            }

            //log all transactions
            Console.WriteLine();
            Console.WriteLine(account.GetAccountHistory());
        }
    }
}
