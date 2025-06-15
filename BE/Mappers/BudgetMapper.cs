using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Dtos.Budget;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Mappers
{
    public class BudgetMapper
    {
        public static BudgetDto MapToBudgetDto(Budget Budget)
        {
            return new BudgetDto
            {
                Amount = Budget.Amount,
                Currency = Budget.Currency,
                Month = Budget.Month,
                CategoryDescription = Budget.CategoryCodeNavigation?.Description ?? "No Description",
                UserId = Budget.UserId
            };
        }

        public static Budget MapPlanningBudgetDtoToModel(PlanningBudgetDto planningBudgetDto)
        {
            return new Budget
            {
                Amount = planningBudgetDto.Amount,
                UserId = planningBudgetDto.UserId,
                CategoryCode = planningBudgetDto.CategoryCode,
                Month = planningBudgetDto.Month
            };
        }
    }
}
