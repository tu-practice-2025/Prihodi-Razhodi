using SummerPracticeWebApi.Enums;

namespace SummerPracticeWebApi.Dtos.Budget
{
    public class BudgetDto
    {
        public uint Amount { get; set; }
        public Currency Currency { get; set; }
        public byte Month { get; set; }
        public string CategoryDescription { get; set; }
        public uint UserId { get; set; }
    }
}