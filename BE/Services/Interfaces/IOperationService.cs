using SummerPracticeWebApi.Dtos;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IOperationService
    {
        Task<IEnumerable<OperationDto>> GetOperationsByUserId(uint userId);
        Task<IEnumerable<OperationDto>> GetOperationsByUserIdAndYear(uint userId, int year);
        Task<IEnumerable<OperationDto>> GetOperationsByUserIdYearAndMonth(uint userId, int month, int year);
    }
}
