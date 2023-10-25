using Domain.VolgaIT.Interfaces;
using Domain.VolgaIT.Entities;
using App.VolgaIT.DTOs;

namespace App.VolgaIT.Mappers
{
    public class UserMapper : IMapper<User, UserDTO>
    {
        public static UserDTO CreateDTOFromEntity(User user)
        {
            return new UserDTO
            {
                UserName = user.Login,
                Password = user.Password
            };
        }
        public static IEnumerable<UserDTO> CreateDTOsFromEntities(IEnumerable<User> users)
        {
            var UserDtoList = new List<UserDTO>();

            foreach(var user in users)
            {
                UserDtoList.Add(CreateDTOFromEntity(user));
            }

            return UserDtoList;
        }
    }
}
