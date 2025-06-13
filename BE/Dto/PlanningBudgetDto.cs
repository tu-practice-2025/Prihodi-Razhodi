using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Models
{
    public class PlanningBudgetDto
    {
        public int Amount { get; set; }
        public Currency Currency { get; set; }
        public bool IsIncome { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string CategoryCode { get; set; }

        public int UserId { get; set; }
    }
}