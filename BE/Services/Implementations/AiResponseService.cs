using SummerPracticeWebApi.Dtos.Budget;
using SummerPracticeWebApi.Models;
using SummerPracticeWebApi.Services.Interfaces;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class AiResponseService : IAiResponseService
    {
        private readonly IPromptSenderService _promptSenderService;
        private readonly IBudgetService _budgetService;
        private readonly ISummaryService _summaryService;

        public AiResponseService(IPromptSenderService promptSenderService, IBudgetService budgetService, ISummaryService summaryService)
        {
            _promptSenderService = promptSenderService;
            _budgetService = budgetService;
            _summaryService = summaryService;
        }
            

        public async Task<string> GetResponse(uint userId)
        {
            var now = DateTime.Now;
            var income = await _summaryService.GetIncomeByMonthAndYear(userId, now.Month, now.Year);
            var operationsByCategories = await _summaryService.GetExpensesCategorised(userId, now.Month, now.Year);
            var budgets = await _budgetService.GetBudgetsByUserIdMonthAndYear(userId, now.Month, now.Year);
            var balance = await _summaryService.GetBalanceSummary(userId);

            IEnumerable<string> lines = budgets.Select(b =>
            {
                var spent = operationsByCategories.TryGetValue(b.CategoryDescription, out var v) ? v : 0m;
                var pct = b.Amount == 0 ? 0 : spent / b.Amount;
                string tag = pct switch
                {
                    > 1.1m => "OVER",
                    < 0.9m => "UNDER",
                    _ => "ON-TRACK"
                };
                return $"{b.CategoryDescription}: {tag} (plan {b.Amount:F0}, spent {spent:F0})";
            });
            string status = string.Join(" · ", lines);

            string system = """
                You are an upbeat personal finance coach. 
                Respond in English. Give exactly **three** short, numbered insights. 
                Start each line with “1.”, “2.”, “3.”.  Speak directly to the client (“you …”).
                No headings, no extra sections.
                """;

            string user = $"""
                Income this month: {income:F0} BGN
                Current balance : {balance:F0} BGN
                Budget status   : {status}
                """;

            return await _promptSenderService.FetchAiResponse(system, user);
        }
    }
}
