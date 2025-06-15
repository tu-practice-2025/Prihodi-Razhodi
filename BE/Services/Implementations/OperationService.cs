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
            return await _context.Operations
                .Where(operation => operation.Acc.UserId == userId)
                .Select(operation => OperationMapper.MapToDto(operation))
                .ToListAsync();
        }
    }
}
