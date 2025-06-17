namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IPromptSenderService
    {
        Task<string> FetchAiResponse(string prompt);
    }
}
