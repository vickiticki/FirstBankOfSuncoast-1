using System;
using System.Linq;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Collections.Generic;


namespace FirstBankOfSuncoast
{
    class TransactionDatabase
    {
        // Transaction list (class)

        private List<Transaction> transactions = new List<Transaction>();

        // Get input from user and add to list

        public void AddTransaction(Transaction newTransaction)
        {
            transactions.Add(newTransaction);

        }

        // to check things 
        public List<Transaction> GetAllTransactions()
        {
            return transactions;
        }

        public List<Transaction> GetSavingsTransactions()
        {
            var savingsList = new List<Transaction>();
            foreach (var tSavings in transactions)
            {
                if (tSavings.AccountType == "savings")
                {
                    savingsList.Add(tSavings);
                }

            }

            return savingsList;
        }

        public List<Transaction> GetCheckingTransactions()
        {
            var checkingList = new List<Transaction>();
            foreach (var tChecking in transactions)
            {
                if (tChecking.AccountType == "checking")
                {
                    checkingList.Add(tChecking);
                }
            }
            return checkingList;
        }


        // THIS PART MIGHT MOVE
        // Method to calculate savings 
        public int GetSavings()
        {
            // Initial balance = 0
            var savingsBalance = 0;
            foreach (var transactionS in transactions)
            {
                if (transactionS.AccountType == "savings")
                {
                    // For each deposit add money to balance
                    if (transactionS.TransactionType == "deposit" || transactionS.TransactionType == "transfer from checking")
                    {
                        savingsBalance += transactionS.TransactionAmount;
                    }
                    else
                    {
                        // For each withdrawal subtract money from balance
                        savingsBalance -= transactionS.TransactionAmount;
                    }


                }
            }
            return savingsBalance;
        }
        // Method to calculate checking
        public int GetChecking()
        {
            // Initial balance = 0
            var checkingBalance = 0;
            foreach (var transactionC in transactions)
            {
                // For each deposit add money to balance
                if (transactionC.AccountType == "checking")
                {
                    if (transactionC.TransactionType == "deposit" || transactionC.TransactionType == "transfer from savings")
                    {
                        checkingBalance += transactionC.TransactionAmount;
                    }
                    else
                    {
                        checkingBalance -= transactionC.TransactionAmount;
                    }
                }

            }
            // For each withdrawal subtract money from balance
            return checkingBalance;

        }
        public void LoadTransactionsFromCSV()
        {
            // Load the list of employees
            if (File.Exists("transactions.csv"))
            {
                var fileReader = new StreamReader("transactions.csv");
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                transactions = csvReader.GetRecords<Transaction>().ToList();
                fileReader.Close();
            }
        }
        public void SaveTransactionsToCSV()
        {
            // Save the list of employees
            var fileWriter = new StreamWriter("transactions.csv");
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(transactions);
            fileWriter.Close();
        }
    }
}