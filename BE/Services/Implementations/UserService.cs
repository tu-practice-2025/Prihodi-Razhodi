using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Dtos.User;
using SummerPracticeWebApi.Mappers;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IncomeExpensesContext _context;

        public UserService(IncomeExpensesContext context)
        {
            _context = context;
        }

        public async Task<UserDto> GetUserByUsername(LoginUserDto loginUserDto)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(user => user.Username == loginUserDto.Username);

            return user != null ? UserMapper.MapToUserDto(user) : null;
        }
    }
}
