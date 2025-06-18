using SummerPracticeWebApi.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class PromptSenderService : IPromptSenderService
    {
        private static readonly HttpClient client = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(1000)
        };

        public async Task<string> FetchAiResponse(string prompt)
        {
            var url = "http://127.0.0.1:9000/completion";

            var requestBody = new
            {
                prompt = prompt,
                n_predict = 500,
                temperature = 0.5
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return responseContent;
                }
                else
                {
                    return $"Error {response.StatusCode}: {responseContent}";
                }
            }
            catch (Exception exception)
            {
                return $"Exception: {exception.Message}";
            }
        }
    }
}
