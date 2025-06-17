namespace SummerPracticeWebApi.Dtos
{
    public class IncomeTransactionDto
    {
        public DateTime Date { get; set; }
        public string Source { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}