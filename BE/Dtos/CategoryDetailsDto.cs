namespace SummerPracticeWebApi.Dtos
{
    public class CategoryDetailsDto
    {
        public decimal TotalSpent { get; set; }
        public List<CategoryTransactionDto> Transactions { get; set; } = new();
    }
}