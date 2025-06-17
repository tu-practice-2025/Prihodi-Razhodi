namespace SummerPracticeWebApi.Dtos
{
    public class CategoryDetailsDto
    {
        public string CategoryDescription { get; set; } = string.Empty;
        public decimal TotalSpent { get; set; }
        public List<CategoryTransactionDto> Transactions { get; set; } = new();
    }
}