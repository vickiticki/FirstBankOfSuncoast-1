using System;
using System.Linq;
using System.Globalization;
using System.IO;
using CsvHelper;


namespace FirstBankOfSuncoast
{
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
                                depositS.TransactionAmount = PromptForInt("How much would you like to deposit? ");
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

                        switch (withdrawAccount)
                        {
                            // Withdraw from checking
                            case "C":
                                Console.WriteLine();
                                // Ask for amount 
                                var amountCW = PromptForInt("How much would you like to withdraw? ");
                                // Get checking balance
                                var balanceC = database.GetChecking();
                                // If ok, send to transaction list
                                if (amountCW <= balanceC)
                                {
                                    var withdrawalC = new Transaction();
                                    withdrawalC.TransactionAmount = amountCW;
                                    withdrawalC.TransactionType = "withdrawal";
                                    withdrawalC.AccountType = "checking";
                                    database.AddTransaction(withdrawalC);

                                }
                                else
                                {
                                    // If more than balance, display error message
                                    Console.WriteLine($"Sorry, you cannot withdraw more than what is in the account. Your checking balance is {balanceC}");
                                }

                                break;
                            // (display new checking balance?)

                            // Withdraw from savings
                            case "S":
                                Console.WriteLine();
                                var amountSW = PromptForInt("How much would you like to withdraw? ");
                                // Get checking balance
                                var balanceS = database.GetSavings();
                                // If ok, send to transaction list
                                if (amountSW <= balanceS)
                                {
                                    var withdrawalS = new Transaction();
                                    withdrawalS.TransactionAmount = amountSW;
                                    withdrawalS.TransactionType = "withdrawal";
                                    withdrawalS.AccountType = "savings";
                                    database.AddTransaction(withdrawalS);

                                }
                                else
                                {
                                    // If more than balance, display error message
                                    Console.WriteLine($"Sorry, you cannot withdraw more than what is in the account. Your checking balance is {balanceS}");
                                }

                                break;
                            // Get savings balance
                            // Ask for amount
                            // If more than balance, display error message
                            // If ok, send to transaction list
                            // (display new savings balance?)

                            case "X":
                                break;

                            default:
                                Console.WriteLine("Error. Please try again.");
                                break;
                        }

                        break;

                    // View balance of both
                    case "V":

                        // Get checking balance
                        // Get savings balance

                        // Display both
                        Console.WriteLine($"You have {database.GetChecking()} in your checking and {database.GetSavings()} in your savings.");

                        // test transactions--DELETE LATER
                        var checkTransactions = database.GetAllTransactions();
                        foreach (var t in checkTransactions)
                        {
                            Console.Write(t.AccountType + " ");
                            Console.Write(t.TransactionType + " ");
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