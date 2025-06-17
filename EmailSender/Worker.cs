namespace EmailSender
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var httpClient = new HttpClient();
            var url = "https://localhost:7121/api/email";

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;

                if (now.Day == 1)
                {
                    try
                    {
                        var response = await httpClient.GetAsync(url, stoppingToken);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            _logger.LogInformation("Emails sent");
                        }
                        else
                        {
                            _logger.LogError("Email failed to send");
                        }
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(exception, "Error while sending email");
                    }

                    var tomorrow = now.Date.AddSeconds(1);
                    var delay = (tomorrow - now).Milliseconds;
                    delay = Math.Abs(delay);
                    await Task.Delay(delay, stoppingToken);
                }
                else
                {
                    _logger.LogError("Today is not the 1 of the month!");
                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                }
            }
        }

    }
}
