using Xunit;

namespace AmountTransaction.Tests
{
    public class Transaction
    {
        public string? Id { get; set; }
        public DateTime Date { get; set; }
        public string? Type { get; set; }
        public decimal Amount { get; set; }
    }
}
