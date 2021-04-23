using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstBankOfSuncoast
{
    class Transaction
    {
        //Amount:
        public int TransactionAmount { get; set; }
        //AccountType:
        public string AccountType { get; set; }
        //TransactionType:
        public string TransactionType { get; set; }


    }
    class TransactionDatabase
    {
        // Transaction list (class)

        private List<Transaction> transactions = new List<Transaction>();

        // to check things 
        public List<Transaction> GetAllTransactions()
        {
            return transactions;
        }

        // Get input from user and add to list
        // (save to csv)
        public void AddTransaction(Transaction newTransaction)
        {
            transactions.Add(newTransaction);

        }

        // THIS PART MIGHT MOVE
        // Method to calculate savings 
        // Initial balance = 0
        // For each deposit add money to balance
        // For each withdrawal subtract money from balance
        // Method to calculate checking
        // Initial balance = 0
        // For each deposit add money to balance
        // For each withdrawal subtract money from balance


    }
    class Program
    {
        static int PromptForInt(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisInteger = Int32.TryParse(Console.ReadLine(), out userInput);
            if (isThisInteger)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Not a number. Please try again.");
                return 0;
            }
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to the First Bank of Suncoast");
            var database = new TransactionDatabase();
            var keepGoing = true;

            while (keepGoing)
            {
                // Display menu with options:
                Console.WriteLine();
                Console.WriteLine("What would you like to do today?");
                Console.WriteLine("(D)eposit   (W)ithdrawal   (V)iew Balance   (Q)uit");
                var response = Console.ReadLine().ToUpper();
                Console.WriteLine();

                switch (response)
                {
                    // Quit
                    case "Q":
                        Console.WriteLine("Thank you. Please come again.");
                        keepGoing = false;
                        break;

                    // Deposit
                    case "D":

                        Console.WriteLine("Would you like to deposit in (C)hecking or (S)avings? E(X)it");
                        var depositAccount = Console.ReadLine().ToUpper();

                        switch (depositAccount)
                        {
                            //Deposit to checking
                            case "C":
                                // Ask for amount 
                                Console.WriteLine();
                                var depositC = new Transaction();

                                depositC.TransactionAmount = PromptForInt("How much would you like to deposit? ");
                                depositC.TransactionType = "deposit";
                                depositC.AccountType = "checking";

                                // Send to transaction list 

                                database.AddTransaction(depositC);
                                // (display new checking balance?)
                                break;
                            // Deposit to savings
                            case "S":
                                Console.WriteLine();

                                var depositS = new Transaction();
                                // Ask for amount 
                                depositS.TransactionAmount = PromptForInt("How much would you like to withdraw? ");
                                depositS.TransactionType = "deposit";
                                depositS.AccountType = "savings";
                                // Send to transaction list
                                database.AddTransaction(depositS);
                                // (display new savings balance?)
                                break;
                            case "X":
                                break;
                            default:
                                Console.WriteLine("Error. Please try again.");
                                break;
                        }

                        break;

                    // Withdrawal
                    case "W":

                        Console.WriteLine("Will you like to withdraw from (C)hecking or (S)avings? E(X)it");
                        var withdrawAccount = Console.ReadLine().ToUpper();
                        // Withdraw from checking
                        // Get checking balance
                        // Ask for amount 
                        // If more than balance, display error message
                        // If ok, send to transaction list
                        // (display new checking balance?)

                        // Withdraw from savings
                        // Get savings balance
                        // Ask for amount
                        // If more than balance, display error message
                        // If ok, send to transaction list
                        // (display new savings balance?)
                        break;

                    // View balance of both
                    case "V":

                        // Get checking balance
                        // Get savings balance

                        // Display both
                        Console.WriteLine($"You have __ in your checking and __ in your savings.");


                        // test transactions--delete later
                        var checkTransactions = database.GetAllTransactions();
                        foreach (var t in checkTransactions)
                        {
                            Console.WriteLine(t.AccountType);
                            Console.WriteLine(t.TransactionType);
                            Console.WriteLine(t.TransactionAmount);
                        }

                        break;

                    default:
                        Console.WriteLine("Error. Please try again.");
                        break;
                }

            }

        }
    }
}