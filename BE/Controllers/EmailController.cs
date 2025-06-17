using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailService _emailService;

    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("test")]
    public async Task<IActionResult> SendTestEmail()
    {
        var success = await _emailService.SendEmailAsync(
            "razhodiprihodi@gmail.com",
            "Test Email",
            "Plain text content",
            "<strong>HTML content</strong>"
        );

        return success ? Ok("Email sent") : StatusCode(500, "Failed to send email");
    }

    [HttpGet("apikey")]
    public IActionResult GetKey()
    {
        var key = _emailService.GetType().GetField("_apiKey", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(_emailService);
        return Ok(new { apiKey = key });
    }

}