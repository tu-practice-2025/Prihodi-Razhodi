using SummerPracticeWebApi.Enums;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Models
{
    public class BudgetDto
    {
        public uint Amount { get; set; }
        public Currency Currency { get; set; }
        public bool IsIncome { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CategoryCode { get; set; }
        public uint UserId { get; set; }
    }
}