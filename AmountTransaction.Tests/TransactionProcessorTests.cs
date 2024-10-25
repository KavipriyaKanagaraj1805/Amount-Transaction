using Xunit;//log frame
using System;      // Required for basic data types like DateTime
using System.Linq;  // Required for LINQ operations like Sum()
using System.Collections.Generic;// Required for using List<T>
using Moq; // Ensure to include Moq for mocking
using AmountTransaction;// Importing the main project namespace

namespace AmountTransaction.Tests//  Define a namespace for test classes
{
    public class TransactionProcessorTests// Test class for TransactionProcessor
    {
        private readonly Mock<TransactionProcessor> _mockProcessor;// Mock object for TransactionProcessor

        public TransactionProcessorTests()
        {
            // Initialize the mock object for TransactionProcessor
            _mockProcessor = new Mock<TransactionProcessor>();


        }

        [Fact]// Attribute indicating this is a test method
        public void AllTransactions_ShouldHaveValidId()// Test method name
        {
            // Arrange: Get the list of transactions from the mocked processor
            var transactions = _mockProcessor.Object.Transactions;

            // Act & Assert: Loop through each transaction to check if the Id is valid
            foreach (var transaction in transactions)
            {
                 // Assert that Id is not null or empty
                Assert.False(string.IsNullOrEmpty(transaction.Id), $"Transaction with Date {transaction.Date} has an invalid Id.");
            }
        }

        [Fact]
        public void AllTransactions_ShouldHaveValidType()// Test for transaction Type validity
        {
           
            var transactions = _mockProcessor.Object.Transactions;

           // Act & Assert: Loop through each transaction to check if the Type is valid
            
            foreach (var transaction in transactions)
            {
                // Assert that Type is not null or empty
                Assert.False(string.IsNullOrEmpty(transaction.Type), $"Transaction with Id {transaction.Id} has an invalid Type.");
            }
        }

        [Fact]
        public void AllTransactions_ShouldHaveValidDate()// Test for transaction Date validity
        {
            var transactions = _mockProcessor.Object.Transactions;

            // Act & Assert: Loop through each transaction to check if the Date is valid
           
            foreach (var transaction in transactions)
            {
                // Assert that Date is not DateTime.MinValue
                Assert.True(transaction.Date != DateTime.MinValue, $"Transaction with Id {transaction.Id} has an invalid Date.");
            }
        }

        [Fact]
        public void AllTransactions_ShouldHaveValidAmount()// Test for transaction Amount validity
        {
            
            var transactions = _mockProcessor.Object.Transactions;

             // Act & Assert: Loop through each transaction to check if the Amount is valid
            
            foreach (var transaction in transactions)
            {
                // Assert that Amount is non-negative
                Assert.True(transaction.Amount >= 0, $"Transaction with Id {transaction.Id} has an invalid Amount: {transaction.Amount}.");
            }
        }
        [Fact]
        public void Transaction_ShouldHaveValidId()// Test to check property assignment
        {
             // Arrange: Create a new Transaction object and assign values
            var transaction = new Transaction
            {
                Id = "1",
                Date = new DateTime(2024, 10, 1),
                Type = "Credit",
                Amount = 100
            };

            // Act &  Assert: Verify that Id and Type can be null
            var isValidId = !string.IsNullOrEmpty(transaction.Id);
            Assert.True(isValidId, $"Transaction with Date {transaction.Date} has an invalid Id.");
        }

        [Fact]
        public void Transaction_ShouldHaveValidDate()
        {
            // Arrange
            var transaction = new Transaction
            {
                Id = "1",
                Date = new DateTime(2024, 10, 1),
                Type = "Credit",
                Amount = 100
            };

            // Act
            var isValidDate = transaction.Date != DateTime.MinValue;

            // Assert
            Assert.True(isValidDate, $"Transaction with Id {transaction.Id} has an invalid Date.");
        }

        [Fact]
        public void Transaction_ShouldHaveValidType()
        {
            // Arrange
            var transaction = new Transaction
            {
                Id = "1",
                Date = new DateTime(2024, 10, 1),
                Type = "Credit",
                Amount = 100
            };

            // Act
            var isValidType = !string.IsNullOrEmpty(transaction.Type);

            // Assert
            Assert.True(isValidType, $"Transaction with Id {transaction.Id} has an invalid Type.");
        }

        [Fact]
        public void Transaction_ShouldHaveNonNegativeAmount()
        {
            // Arrange
            var transaction = new Transaction
            {
                Id = "1",
                Date = new DateTime(2024, 10, 1),
                Type = "Credit",
                Amount = 100
            };

            // Act
            var isValidAmount = transaction.Amount >= 0;

            // Assert
            Assert.True(isValidAmount, $"Transaction with Id {transaction.Id} has an invalid Amount: {transaction.Amount}.");
        }

        [Fact]
        public void Transaction_SetProperties_ShouldAssignValues()
        {
            // Arrange
            var transaction = new Transaction
            {
                Id = "1",
                Date = new DateTime(2024, 10, 1),
                Type = "Credit",
                Amount = 100.50m
            };

            // Act & Assert
            Assert.Equal("1", transaction.Id);
            Assert.Equal(new DateTime(2024, 10, 1), transaction.Date);
            Assert.Equal("Credit", transaction.Type);
            Assert.Equal(100.50m, transaction.Amount);
        }

        [Fact]
        public void Transaction_SetNullableProperties_ShouldAllowNullValues()
        {
            // Arrange
            var transaction = new Transaction
            {
                // Id = null,
                // Type = null,
                Date = DateTime.Now,
                Amount = 50m
            };

            // Act & Assert
            Assert.Null(transaction.Id);
            Assert.Null(transaction.Type);
            Assert.Equal(50m, transaction.Amount);
        }

        [Fact]
        public void Transaction_SetNegativeAmount_ShouldBeAllowed()
        {
            // Arrange
            var transaction = new Transaction
            {
                Id = "2",
                Date = DateTime.Now,
                Type = "Debit",
                Amount = -25m
            };

            // Act & Assert
            Assert.Equal(-25m, transaction.Amount);
        }

        [Fact]
        public void Transaction_SetPositiveAmount_ShouldBeAllowed()
        {
            // Arrange
            var transaction = new Transaction
            {
                Id = "3",
                Date = DateTime.Now,
                Type = "Credit",
                Amount = 150m
            };

            // Act & Assert
            Assert.Equal(150m, transaction.Amount);
        }

        [Fact]
        public void Transaction_Id_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var transaction = new Transaction();
            transaction.Id = "test_id";

            // Act & Assert
            Assert.Equal("test_id", transaction.Id);
        }

        [Fact]
        public void Transaction_Type_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var transaction = new Transaction();
            transaction.Type = "Credit";

            // Act & Assert
            Assert.Equal("Credit", transaction.Type);
        }

        [Fact]
        public void Transaction_Date_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var transaction = new Transaction();
            var date = new DateTime(2024, 10, 1);
            transaction.Date = date;

            // Act & Assert
            Assert.Equal(date, transaction.Date);
        }

        [Fact]
        public void Transaction_Amount_ShouldBeSetAndGetCorrectly()
        {
            // Arrange
            var transaction = new Transaction();
            transaction.Amount = 200.75m;

            // Act & Assert
            Assert.Equal(200.75m, transaction.Amount);
        }

        [Fact]
        public void Transaction_WithDifferentData_ShouldBeUnique()
        {
            // Arrange
            var transaction1 = new Transaction { Id = "1", Date = DateTime.Now, Type = "Debit", Amount = 100m };
            var transaction2 = new Transaction { Id = "2", Date = DateTime.Now, Type = "Credit", Amount = 200m };

            // Act & Assert
            Assert.NotEqual(transaction1.Id, transaction2.Id);
            Assert.NotEqual(transaction1.Amount, transaction2.Amount);
            Assert.NotEqual(transaction1.Type, transaction2.Type);
        }

        [Fact]
        public void Transaction_CanBeUsedInCollections()// Test for setting and getting Amount
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { Id = "1", Date = DateTime.Now, Type = "Credit", Amount = 50m },
                new Transaction { Id = "2", Date = DateTime.Now.AddDays(1), Type = "Debit", Amount = 20m }
            };
            
            var totalAmount = transactions.Sum(t => t.Amount);
            Assert.Equal(70m, totalAmount);
        }

    }
}
