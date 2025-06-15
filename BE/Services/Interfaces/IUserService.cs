using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Dtos.User;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByUsername(LoginUserDto loginUserDto);
    }
}
