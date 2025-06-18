namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IAiResponseService
    {
        Task<string> GetResponse(uint userId);
    }
}
