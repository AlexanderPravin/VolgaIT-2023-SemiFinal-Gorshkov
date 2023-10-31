using Domain.VolgaIT.Interfaces;
using Domain.VolgaIT.Entities;
using App.VolgaIT.DTOs;

namespace App.VolgaIT.Mappers
{
    public class UserMapper : IMapper<User, UserResponseDTO>
    {
        public static UserResponseDTO CreateDTOFromEntity(User user)
        {
            return new UserResponseDTO
            {
                Id = user.Id.ToString(),
                Login = user.Login,
                Password = user.Password,
                Balance = user.Balance,
                Role = user.Role.ToString()
            };
        }

        public static IEnumerable<UserResponseDTO> CreateDTOsFromEntities(IEnumerable<User> users)
        {
            var UserDtoList = new List<UserResponseDTO>();

            foreach(var user in users)
            {
                UserDtoList.Add(CreateDTOFromEntity(user));
            }

            return UserDtoList;
        }
    }
}
