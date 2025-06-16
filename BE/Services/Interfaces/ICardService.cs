using SummerPracticeWebApi.Dtos;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<CardDto>> GetCardsByAccountId(uint accountId);
    }
}
