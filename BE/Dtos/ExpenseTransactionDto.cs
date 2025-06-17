namespace SummerPracticeWebApi.Dtos
{
    public class ExpenseTransactionDto
    {
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }
}