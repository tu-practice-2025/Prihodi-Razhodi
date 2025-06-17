namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface ISummaryService
    {
        Task<decimal> GetExpensesByMonthAndYear(uint userId, int month, int year);
        Task<decimal> GetIncomeByMonthAndYear(uint userId, int month, int year);
        Task<decimal> GetBalanceSummary(uint userId);
        Task<Dictionary<string, decimal>> GetExpensesCategorised(uint userId, int month, int year);
    }
}
