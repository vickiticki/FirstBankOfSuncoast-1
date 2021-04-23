using System.Collections.Generic;


namespace FirstBankOfSuncoast
{
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
        public int GetSavings()
        {
            // Initial balance = 0
            var savingsBalance = 0;
            foreach (var transactionS in transactions)
            {
                if (transactionS.AccountType == "savings")
                {
                    // For each deposit add money to balance
                    if (transactionS.TransactionType == "deposit")
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
                    if (transactionC.TransactionType == "deposit")
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

    }
}