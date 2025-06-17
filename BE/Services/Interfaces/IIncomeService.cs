using SummerPracticeWebApi.Dtos;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IIncomeService
    {
        Task<List<DailyIncomeDto>> GetUserIncomeByDayAsync(uint userId, byte month, uint year);
        Task<List<IncomeTransactionDto>> GetLatestIncomesAsync(uint userId, byte month, uint year, int skip, int take);
    }
}