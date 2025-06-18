using SummerPracticeWebApi.Dtos.Budget;
using SummerPracticeWebApi.Models;
using SummerPracticeWebApi.Services.Interfaces;
using System.Text.Json;

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

            var testPrompt = "Expenses by categories: " + JsonSerializer.Serialize(operationsByCategories) + 
                "\nBudgets by categories: " + JsonSerializer.Serialize(budgets) + 
                "\nCurrent balance: " + balance.ToString();


            var prompt = $"Analize the data below:\nIncome: {income}\nBudget: {JsonSerializer.Serialize(budgets)}\nExpenses: {JsonSerializer.Serialize(operationsByCategories)}\nIdentify exactly 3 categories where expenses exceed budget (ignore fixed housing).\r\nFor each, write one concise, specific sentence advising how much to reduce spending next month and why, referencing the difference in euros.\r\nDo not add general tips or introductions. Return only the 3 advice sentences.";

            return await _promptSenderService.FetchAiResponse(prompt);
        }
    }
}
