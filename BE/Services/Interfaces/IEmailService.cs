namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SendEmailByUserId(uint userId);
        public Task<bool> SendEmailsToAllUsers();
    }
}
