namespace SummerPracticeWebApi.Dtos
{
    public class CategoryTransactionDto
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}