using Microsoft.AspNetCore.Mvc;
using SummerPracticeWebApi.Services.Implementations;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailService _emailService;
    private readonly OperationService operationService;

    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> SendEmail(uint userId)
    {
        var success = await _emailService.SendEmailAsync(userId);

        return success ? Ok("Email sent") : StatusCode(500, "Failed to send email");
    }

    //[HttpGet("apikey")]
    //public IActionResult GetKey()
    //{
    //    var key = _emailService.GetType().GetField("_apiKey", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(_emailService);
    //    return Ok(new { apiKey = key });
    //}

}