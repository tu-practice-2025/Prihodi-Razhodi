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

            var operationsByCategories = await _summaryService.GetExpensesCategorised(userId, now.Month, now.Year);
            var budgets = await _budgetService.GetBudgetsByUserIdMonthAndYear(userId, now.Month, now.Year);
            var balance = await _summaryService.GetBalanceSummary(userId);

            var prompt = "Expenses by categories: " + JsonSerializer.Serialize(operationsByCategories) + 
                "\nBudgets by categories: " + JsonSerializer.Serialize(budgets) + 
                "\nCurrent balance: " + balance.ToString();


            return await _promptSenderService.FetchAiResponse(prompt);
        }
    }
}
