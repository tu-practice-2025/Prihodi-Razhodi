using SummerPracticeWebApi.Enums;

namespace SummerPracticeWebApi.Dtos.Budget
{
    public class BudgetDto
    {
        public uint Id { get; set; }
        public uint Amount { get; set; }
        public Currency Currency { get; set; }
        public byte Month { get; set; }
        public uint Year { get; set; }
        public string CategoryDescription { get; set; }
        public uint UserId { get; set; }
        public string CategoryCode { get; set; }
    }
}