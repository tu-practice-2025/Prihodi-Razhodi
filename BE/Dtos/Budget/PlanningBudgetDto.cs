using SummerPracticeWebApi.Enums;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Models
{
    public class PlanningBudgetDto
    {
        public uint Amount { get; set; }
        public uint UserId { get; set; }
        public string CategoryCode { get; set; }
        public byte Month { get; set; }
    }
}