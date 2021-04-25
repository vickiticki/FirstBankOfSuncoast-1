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
            var database = new TransactionDatabase();
            database.LoadTransactionsFromCSV();
            Console.WriteLine("Welcome to the First Bank of Suncoast");
            var keepGoing = true;

            while (keepGoing)
            {
                // Display menu with options:
                Console.WriteLine();
                Console.WriteLine("What would you like to do today?");
                Console.WriteLine("(D)eposit   (W)ithdrawal   (T)ransfer  (V)iew Balance   (Q)uit");
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
                                // check for negatives
                                if (depositC.TransactionAmount <= 0)
                                {
                                    Console.WriteLine("Sorry, deposits must be greater than zero.");
                                }
                                else
                                {
                                    // Send to transaction list 

                                    database.AddTransaction(depositC);
                                    // (display new checking balance?)
                                    database.SaveTransactionsToCSV();
                                }
                                break;
                            // Deposit to savings
                            case "S":
                                Console.WriteLine();

                                var depositS = new Transaction();
                                // Ask for amount 
                                depositS.TransactionAmount = PromptForInt("How much would you like to deposit? ");
                                depositS.TransactionType = "deposit";
                                depositS.AccountType = "savings";
                                // check for negatives
                                if (depositS.TransactionAmount <= 0)
                                {
                                    Console.WriteLine("Sorry, deposits must be greater than zero.");
                                }
                                else
                                {
                                    // Send to transaction list
                                    database.AddTransaction(depositS);
                                    // (display new savings balance?)
                                    database.SaveTransactionsToCSV();
                                }
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
                                    // check for negatives
                                    if (withdrawalC.TransactionAmount <= 0)
                                    {
                                        Console.WriteLine("Sorry, withdrawals must be greater than zero.");
                                    }
                                    else
                                    {
                                        database.AddTransaction(withdrawalC);
                                        database.SaveTransactionsToCSV();
                                    }

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
                                    // check for negatives
                                    if (withdrawalS.TransactionAmount <= 0)
                                    {
                                        Console.WriteLine("Sorry, withdrawals must be greater than zero.");
                                    }
                                    else
                                    {
                                        database.AddTransaction(withdrawalS);
                                        database.SaveTransactionsToCSV();
                                    }

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
                    case "T":

                        // ask where to transfer
                        Console.WriteLine();
                        Console.WriteLine("Would you like to (A) transfer from savings to checking, or (B) transfer from checking to savings? (R)eturn to Main Menu");
                        var transferWhere = Console.ReadLine().ToUpper();

                        Console.WriteLine();

                        switch (transferWhere)
                        {
                            case "A":
                                // savings to checking

                                var transferAmountToC = PromptForInt("How much would you like to transfer?");

                                var balanceS = database.GetSavings();
                                if (transferAmountToC <= balanceS)
                                {
                                    // make transfer
                                    var transferToChecking = new Transaction();
                                    transferToChecking.TransactionAmount = transferAmountToC;
                                    transferToChecking.TransactionType = "transfer to checking";
                                    transferToChecking.AccountType = "savings";
                                    database.AddTransaction(transferToChecking);

                                    var transferFromSavings = new Transaction();
                                    transferFromSavings.TransactionAmount = transferAmountToC;
                                    transferFromSavings.TransactionType = "transfer from savings";
                                    transferFromSavings.AccountType = "checking";
                                    database.AddTransaction(transferFromSavings);

                                    database.SaveTransactionsToCSV();

                                }
                                else
                                {
                                    Console.WriteLine("Sorry, you cannot transfer more than you have in savings.");
                                }

                                break;
                            case "B":
                                // checking to savings
                                var transferAmountToS = PromptForInt("How much would you like to transfer?");

                                var balanceC = database.GetChecking();
                                if (transferAmountToS <= balanceC)
                                {
                                    // make transfer
                                    var transferFromChecking = new Transaction();
                                    transferFromChecking.TransactionAmount = transferAmountToS;
                                    transferFromChecking.TransactionType = "transfer from checking";
                                    transferFromChecking.AccountType = "savings";
                                    database.AddTransaction(transferFromChecking);

                                    var transferToSavings = new Transaction();
                                    transferToSavings.TransactionAmount = transferAmountToS;
                                    transferToSavings.TransactionType = "transfer to savings";
                                    transferToSavings.AccountType = "checking";
                                    database.AddTransaction(transferToSavings);

                                    database.SaveTransactionsToCSV();

                                }
                                else
                                {
                                    Console.WriteLine("Sorry, you cannot transfer more than you have in checking.");
                                }
                                break;
                            default:
                                break;
                        }
                        // ask amount
                        break;
                    case "V":

                        // Get checking balance
                        // Get savings balance

                        // Display both
                        Console.WriteLine($"You have {database.GetChecking()} in your checking and {database.GetSavings()} in your savings.");
                        Console.WriteLine();
                        Console.WriteLine("Would you like to see your (C)hecking transactions, (S)avings transaction, or (R)eturn to main menu? ");
                        var choice = Console.ReadLine().ToUpper();
                        switch (choice)
                        {
                            case "S":
                                Console.WriteLine();
                                Console.WriteLine("Savings Transactions:");
                                var savingTransactions = database.GetSavingsTransactions();
                                if (savingTransactions.Count == 0)
                                {
                                    Console.WriteLine("No Transactions");
                                }
                                else
                                {
                                    foreach (var st in savingTransactions)
                                    {
                                        Console.Write(st.TransactionType + ":  ");
                                        Console.WriteLine("$" + st.TransactionAmount);
                                    }
                                }
                                break;
                            case "C":
                                Console.WriteLine();
                                Console.WriteLine("Checking Transactions");
                                var checkingTransactions = database.GetCheckingTransactions();
                                if (checkingTransactions.Count == 0)
                                {
                                    Console.WriteLine("No Transactions");
                                }
                                else
                                {
                                    foreach (var ct in checkingTransactions)
                                    {
                                        Console.Write(ct.TransactionType + ":  ");
                                        Console.WriteLine("$" + ct.TransactionAmount);
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                        // test transactions--DELETE LATER
                        // var checkTransactions = database.GetAllTransactions();
                        // foreach (var t in checkTransactions)
                        // {
                        //     Console.Write(t.AccountType + " ");
                        //     Console.Write(t.TransactionType + " ");
                        //     Console.WriteLine(t.TransactionAmount);
                        // }

                        break;

                    default:
                        Console.WriteLine("Error. Please try again.");
                        break;
                }

            }

        }
    }
}