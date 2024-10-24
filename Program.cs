using System;
using AmountTransaction;
using NLog; // Ensure NLog is included

class Program
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    static void Main(string[] args)
    {
        logger.Info("Application started.");

        // Hardcoded path to the JSON file
        string filePath = "transactions.json";
        var processor = new TransactionProcessor();

        try
        {
            // Load transactions using the hardcoded file path
            processor.LoadTransactions(filePath);

            // Log the results
            var totalCredits = processor.GetTotalCredits();//Calculate the sum of totla credit amount from the transaction json file 
            logger.Info("Total Credit Amount: {Total}", totalCredits);
            Console.WriteLine("Total Credit Amount: " + totalCredits);

            var totalDebits = processor.GetTotalDebits();
            logger.Info("Total Debit Amount: {Total}", totalDebits);
            Console.WriteLine("Total Debit Amount: " + totalDebits);

            var highestTransactionDate = processor.GetDateOfHighestTransaction();
            logger.Info("Date of Highest Transaction: {Date}", highestTransactionDate);
            Console.WriteLine("Date of Highest Transaction: " + highestTransactionDate);

            var averageAmountPerDay = processor.GetAverageAmountPerDay();
            logger.Info("Average Amount per Day: {Average}", averageAmountPerDay);
            Console.WriteLine("Average Amount per Day: " + averageAmountPerDay);

            var top5Dates = processor.GetTop5DatesWithHighestTotal();
            logger.Info("Top 5 Dates with Highest Total Amount:");
            Console.WriteLine("Top 5 Dates with Highest Total Amount:");
            foreach (var (date, totalAmount) in top5Dates)
            {
                logger.Info("Date: {Date}, Total Amount: {Total}", date.ToShortDateString(), totalAmount);
                Console.WriteLine($"Date: {date.ToShortDateString()}, Total Amount: {totalAmount}");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "An error occurred during processing.");
            Console.WriteLine("An error occurred: " + ex.Message); // Print error message
        }
        finally
        {
            logger.Info("Application finished.");
        }
    }
}
