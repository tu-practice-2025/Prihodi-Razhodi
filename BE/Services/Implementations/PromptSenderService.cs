using System.Text.Json;
using System.Net.Http.Json;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Services.Implementations
{
    public sealed class PromptSenderService : IPromptSenderService
    {
        private static readonly HttpClient client = new()
        {
            BaseAddress = new Uri("http://localhost:9000"),
            Timeout = TimeSpan.FromSeconds(100)
        };

        public async Task<string> FetchAiResponse(string systemPrompt, string userPrompt)
        {
            var body = new
            {
                messages = new[] {
                    new { role = "system", content = systemPrompt },
                    new { role = "user",   content = userPrompt   }
                },
                max_tokens = 160,
                temperature = 0.65,
                presence_penalty = 0.3,
                top_k = 25,
                top_p = 1,
                repetition_penalty = 1.1,
            };

            using var res = await client.PostAsJsonAsync("/v1/chat/completions", body);

            res.EnsureSuccessStatusCode();

            await using var stream = await res.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            var content = doc.RootElement
                             .GetProperty("choices")[0]
                             .GetProperty("message")
                             .GetProperty("content")
                             .GetString()!
                             .Trim();

            return content;
        }
    }
}
