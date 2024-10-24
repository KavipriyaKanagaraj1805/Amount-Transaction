using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace AmountTransaction
{
    public class Transaction
    {
        public string Id { get; set; } = string.Empty; // Initialized to an empty string
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty; // Initialized to an empty string
        public decimal Amount { get; set; }
    }

    public class TransactionProcessor
    {
        public List<Transaction> Transactions { get; set; } // A list to store multiple transactions

        public TransactionProcessor()
        {
            Transactions = new List<Transaction>();// Initialize the list in the constructor
        }

        public void LoadTransactions(string filePath)
        {
            
            filePath = "D:/.NetWorkspace/AmountTransaction/transactions.json";// Path to the JSON file

            //Check if the file exists at the specified path
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            try
            {
                // Read all the content from the file
                var jsonContent = File.ReadAllText(filePath);

                // Deserialize JSON content into a list of transactions - Conversion
                var transactions = JsonConvert.DeserializeObject<List<Transaction>>(jsonContent);

                // Assign the deserialized list to Transactions if it's not null
                if (transactions != null)
                {
                    Transactions = transactions;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file reading or deserialization
                throw new Exception($"Error loading transactions from file: {filePath}. Details: {ex.Message}");
            }
        }
        // Filter the transactions where the Type is "Credit" and sum the amounts
        public decimal GetTotalCredits()
        {
            return Transactions.Where(t => t.Type?.Equals("Credit", StringComparison.OrdinalIgnoreCase) == true).Sum(t => t.Amount);
        }
        // Filter the transactions where the Type is "Debit" and sum the amounts    
        public decimal GetTotalDebits()
        {
            return Transactions.Where(t => t.Type?.Equals("Debit", StringComparison.OrdinalIgnoreCase) == true).Sum(t => t.Amount);
        }

        public DateTime GetDateOfHighestTransaction()
        {
            // Order the transactions by Amount in descending order and pick the first (largest)
            var highestTransaction = Transactions.OrderByDescending(t => t.Amount).FirstOrDefault();
               // Return the Date of the highest transaction or DateTime.MinValue if no transactions are found
   
            return highestTransaction?.Date ?? DateTime.MinValue;
        }

        public decimal GetAverageAmountPerDay()
        {
             // Group transactions by the Date and calculate the average amount
            var groupedByDate = Transactions.GroupBy(t => t.Date.Date);
            var groupByAmount = Transactions.Sum(t => t.Amount);
            var datecount = groupedByDate.Count()+1;
            var averageValue = groupByAmount/datecount;
            // Return the average or 0 if there are no transactions
            return groupedByDate.Any() ? Transactions.Sum(t => t.Amount) / (groupedByDate.Count()+1) : 0;
        }

        public IEnumerable<(DateTime Date, decimal TotalAmount)> GetTop5DatesWithHighestTotal()
        {
             // Group by date, calculate the total for each date, and return the top 5 dates with the highest total amounts
            return Transactions.GroupBy(t => t.Date.Date)
                              .Select(g => (Date: g.Key, TotalAmount: g.Sum(t => t.Amount)))
                              .OrderByDescending(x => x.TotalAmount)
                              .Take(5);
        }
    }
}