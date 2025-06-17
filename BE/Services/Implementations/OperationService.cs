using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Mappers;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class OperationService : IOperationService
    {
        private readonly IncomeExpensesContext _context;

        public OperationService(IncomeExpensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OperationDto>> GetOperationsByUserId(uint userId)
        {
            var operations = await _context.Operations
                .Where(operation => operation.Acc.UserId == userId)
                .Include(operation => operation.CategoryCodeNavigation)
                .Include(operation => operation.Card)
                .Include(operation => operation.Acc)
                .ToListAsync();

            return operations.Select(OperationMapper.MapToDto);
        }

        public async Task<IEnumerable<OperationDto>> GetOperationsByUserIdAndYear(uint userId, int year)
        {
            var operations = await _context.Operations
                .Where(operation => operation.Acc.UserId == userId &&
                       operation.DateTime.Year == year)
                .Include(operation => operation.CategoryCodeNavigation)
                .Include(operation => operation.Card)
                .Include(operation => operation.Acc)
                .ToListAsync();

            return operations.Select(OperationMapper.MapToDto);
        }

        public async Task<IEnumerable<OperationDto>> GetOperationsByUserIdYearAndMonth(uint userId, int month, int year)
        {
            var operations = await _context.Operations
                .Where(operation => operation.Acc.UserId == userId &&
                       operation.DateTime.Month == month &&
                       operation.DateTime.Year == year)
                .Include(operation => operation.CategoryCodeNavigation)
                .Include(operation => operation.Card)
                .Include(operation => operation.Acc)
                .ToListAsync();

            return operations.Select(OperationMapper.MapToDto);
        }

        public async Task<Dictionary<string, List<OperationDto>>> GetOperationsByCategory(uint userId, int month, int year)
        {
            var operations = await _context.Operations
                .Where(operations => operations.Acc.UserId == userId &&
                       operations.DateTime.Month == month &&
                       operations.DateTime.Year == year)
                .Include(operation => operation.CategoryCodeNavigation)
                .Include(operation => operation.Card)
                .Include(operation => operation.Acc)
                .ToListAsync();

            return operations.GroupBy(operation => operation.CategoryCode ?? "OTHR")
                .ToDictionary(group => group.Key, group => group.Select(OperationMapper.MapToDto).ToList());
        }

        public async Task<IEnumerable<OperationDto>> GetExpensesByMonthAndYear(uint userId, int month, int year)
        {
            var expenses = await _context.Operations
                .Where(operation => operation.Acc.UserId == userId &&
                    operation.IsExpense == true &&
                    operation.DateTime.Month == month &&
                    operation.DateTime.Year == year)
                .Include(operation => operation.Acc)
                .Include(operation => operation.CategoryCodeNavigation)
                .Include(operation => operation.Card)
                .ToListAsync();

            return expenses.Select(OperationMapper.MapToDto);
        }

        public async Task<IEnumerable<OperationDto>> GetIncomesByMonthAndYear(uint userId, int month, int year)
        {
            var incomes = await _context.Operations
                .Where(operation => operation.Acc.UserId == userId &&
                    operation.IsExpense == false &&
                    operation.DateTime.Month == month &&
                    operation.DateTime.Year == year)
                .Include(operation => operation.Acc)
                .Include(operation => operation.CategoryCodeNavigation)
                .Include(operation => operation.Card)
                .ToListAsync();

            return incomes.Select(OperationMapper.MapToDto);
        }
    }
}
