using Microsoft.AspNetCore.Mvc;
using SummerPracticeWebApi.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> SendEmail(uint userId)
    {
        var success = await _emailService.SendEmailByUserId(userId);

        return success ? Ok("Email sent") : StatusCode(500, "Failed to send email");
    }

    [HttpGet("")]
    public async Task<IActionResult> SendEmailsToAllUsers()
    {
        var success = await _emailService.SendEmailsToAllUsers();

        return success ? Ok("Email sent") : StatusCode(500, "Failed to send email");
    }
}