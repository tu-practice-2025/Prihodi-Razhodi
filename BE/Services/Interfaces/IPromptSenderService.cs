namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IPromptSenderService
    {
        Task<string> FetchAiResponse(string promptSystem, string promptUser);
    }
}
