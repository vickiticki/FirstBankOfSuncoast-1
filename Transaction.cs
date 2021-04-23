using System;
using System.Linq;
using System.Globalization;
using System.IO;
using CsvHelper;

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
}