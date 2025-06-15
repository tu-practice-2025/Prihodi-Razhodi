using SummerPracticeWebApi.Dtos.User;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Mappers
{
    public class UserMapper
    {
        public static User MapToModel(UserDto UserDto)
        {
            return new User
            {
                Username = UserDto.Username
            };
        }

        public static UserDto MapToDto(User User)
        {
            return new UserDto
            {
                Id = User.Id,
                Username = User.Username,
                FirstName = User.FirstName,
                MiddleName = User.MiddleName,
                LastName = User.LastName,
                Egn = User.Egn.Substring(8)
            };
        }
    }
}
