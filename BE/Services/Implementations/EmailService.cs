using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService
{
    private readonly string _apiKey;

    public EmailService(IConfiguration configuration)
    {
        _apiKey = " ";
    }

    public async Task<bool> SendEmailAsync(string toEmail, string subject, string plainText, string htmlContent)
    {
        var client = new SendGridClient(_apiKey);

        var options = new SendGridClientOptions
        {
            ApiKey = _apiKey
        };
        options.SetDataResidency("eu");


        var from = new EmailAddress("razhodiprihodi@gmail.com", "Expense Tracker");
        var to = new EmailAddress("group-39-ps@abv.bg", "Kalina");
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainText, htmlContent);
        var response = await client.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }
}
