using SummerPracticeWebApi.Dtos;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategorySpendingDto>> GetUserSpendingsByCategoryAsync(uint userId, byte month, uint year);
        Task<List<ExpenseTransactionDto>> GetLatestExpensesAsync(uint userId, byte month, uint year);
    }
}