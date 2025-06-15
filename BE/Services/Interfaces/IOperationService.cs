using SummerPracticeWebApi.Dtos;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IOperationService
    {
        Task<IEnumerable<OperationDto>> GetOperationsByUserId(uint UserId);
    }
}
