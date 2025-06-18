using SummerPracticeWebApi.Dtos;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IOperationService
    {
        Task<IEnumerable<OperationDto>> GetOperationsByUserId(uint userId);
        Task<IEnumerable<OperationDto>> GetOperationsByUserIdAndYear(uint userId, int year);
        Task<IEnumerable<OperationDto>> GetOperationsByUserIdYearAndMonth(uint userId, int month, int year);
        Task<Dictionary<string, List<OperationDto>>> GetOperationsByCategory(uint userId, int month, int year);
        Task<IEnumerable<OperationDto>> GetExpensesByMonthAndYear(uint userId, int month, int year);
        Task<IEnumerable<OperationDto>> GetIncomesByMonthAndYear(uint userId, int month, int year);
    }
}
