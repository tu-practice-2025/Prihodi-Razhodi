using SendGrid;
using SendGrid.Helpers.Mail;
using SummerPracticeWebApi.Dtos.User;
using SummerPracticeWebApi.Services.Interfaces;
using System.Text;

public class EmailService : IEmailService
{
    private readonly string _apiKey;
    private readonly IOperationService _operationService;
    private readonly ISummaryService _summaryService;
    private readonly IUserService _userService;

    public EmailService(
        IConfiguration configuration,
        IOperationService operationService,
        ISummaryService summaryService,
        IUserService userService)
    {
        _apiKey = configuration["SendGrid:ApiKey"]
                  ?? throw new InvalidOperationException("SendGrid API key missing");
        _operationService = operationService;
        _summaryService = summaryService;
        _userService = userService;
    }

    public async Task<bool> SendEmailByUserId(uint userId)
    {
        var user = await _userService.GetUserByUserId(userId);

        var now = DateTime.Now;
        var month = now.Month - 1;
        var year = now.Year - 1;

        var operationsByCategory = await _operationService
            .GetOperationsByCategory(userId, month, year);

        // 2) build email body
        var sb = new StringBuilder();
        sb.AppendLine($"<h2>Месечен отчет за {now:MMMM yyyy}</h2>");
        sb.AppendLine($"<p><strong>Общо приходи:</strong> {_summaryService.GetIncomeByMonthAndYear(userId, month, year).Result}</p>");
        sb.AppendLine($"<p><strong>Общо разходи:</strong> {_summaryService.GetExpensesByMonthAndYear(userId, month, year).Result}</p>");
        sb.AppendLine($"<p><strong>Общ баланс:</strong> {_summaryService.GetBalanceSummary(userId).Result}</p>");

        sb.AppendLine("<h3>Разходи по категории</h3>");
        sb.AppendLine("<ul>");
        foreach (var pair in operationsByCategory)
        {
            var categoryCode = pair.Key;
            var dtos = pair.Value;
            var total = dtos.Sum(d => d.AmountLcy);
            sb.AppendLine($"  <li>{categoryCode}: {total:C}</li>");
        }
        sb.AppendLine("</ul>");

        // 3) send via SendGrid
        var client = new SendGridClient(new SendGridClientOptions
        {
            ApiKey = _apiKey,
        });

        var from = new EmailAddress("razhodiprihodi@gmail.com", "Expense Tracker");
        var to = new EmailAddress(user.Email, $"{user.FirstName} {user.LastName}");
        var msg = MailHelper.CreateSingleEmail(
            from, to,
            subject: $"Вашият отчет за {now:MMMM yyyy}",
            plainTextContent: sb.ToString(),
            htmlContent: sb.ToString()
        );

        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendEmailsToAllUsers()
    {
        var users = _userService.GetAllUsers().Result;

        foreach(UserDto user in users)
        {
            await SendEmailByUserId(user.Id);
        }

        return true;
    }
}
