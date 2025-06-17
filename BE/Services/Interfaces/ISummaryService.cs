namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface ISummaryService
    {
        Task<decimal> getExpensesByMonthAndYear(uint userId, int month, int year);
        Task<decimal> getBalanceSummary(uint userId);
    }
}
