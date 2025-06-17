using SendGrid;
using SendGrid.Helpers.Mail;
using SummerPracticeWebApi.Services.Implementations;
using SummerPracticeWebApi.Services.Interfaces;

public class EmailService
{
    private readonly string _apiKey;

    private readonly IOperationService _operationService;

    private readonly IUserService _userService;

    public EmailService(IConfiguration configuration, IOperationService operationService, IUserService userService)
    {
        _apiKey = configuration["SendGrid:ApiKey"];
        _operationService = operationService;
        _userService = userService;
    }

    public async Task<bool> SendEmailAsync(uint userId)
    {
        var client = new SendGridClient(_apiKey);

        var options = new SendGridClientOptions
        {
            ApiKey = _apiKey
        };
        options.SetDataResidency("eu");

        var userEmail = _userService.GetUserByUserId(userId).Result.Email;
        Console.WriteLine(userEmail);

        DateTime date = DateTime.Now;
        var expenses = _operationService.getExpensesByMonthAndYear(userId, date.Month, date.Year);
        var incomes = _operationService.getIncomesByMonthAndYear(userId, date.Month, date.Year);

        var from = new EmailAddress("razhodiprihodi@gmail.com", "Expense Tracker");
        var to = new EmailAddress(userEmail, userEmail);
        var email = MailHelper.CreateSingleEmail(from, to, "Monthly Report", (expenses.ToString() + incomes.ToString()), "");
        var response = await client.SendEmailAsync(email);
        return response.IsSuccessStatusCode;
    }
}
