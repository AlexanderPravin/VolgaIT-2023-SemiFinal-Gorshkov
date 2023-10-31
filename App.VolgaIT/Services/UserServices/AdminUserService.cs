using App.VolgaIT.DTOs;
using App.VolgaIT.Helpers;
using Domain.VolgaIT.Options;
using Domain.VolgaIT.Settings;
using Infrastructure.VolgaIT;
using App.VolgaIT.Mappers;
using Domain.VolgaIT.Entities;

namespace App.VolgaIT.Services
{
    public class AdminUserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PasswordHelper _passwordHelper;

        public AdminUserService(UnitOfWork unitOfWork, PasswordHashOptions passwordHashOptions)
        {
            _unitOfWork = unitOfWork;
            _passwordHelper = new PasswordHelper(passwordHashOptions);
        }

        public async Task RegisterUserAsync(AdminUserRequestDTO dto)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByAsync(x => x.Login == dto.UserName);
            if (user is not null)
                throw new ArgumentException("Login should be unique ");

            user = new()
            {
                Id = Guid.NewGuid(),
                Login = dto.UserName,
                Password = _passwordHelper.GetPassword(dto.Password),
                Balance = dto.Balance
            };
            if (dto.IsAdmin) user.Role = UserRole.Admin;

            _unitOfWork.UserRepository.AddEntity(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserResponseDTO> GetUserByIdAsync(string id)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id) ?? throw new ArgumentException($"Can`t find user by {id}");

            return UserMapper.CreateDTOFromEntity(user);
        }

        public async Task UpdateUserAsync(AdminUserRequestDTO dto, string login)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByAsync(x => x.Login == login) ?? throw new ArgumentException($"Can`t find user with {login} login");

            user.Login = dto.UserName;
            user.Password = dto.Password;
            user.Role = dto.IsAdmin ? UserRole.Admin : UserRole.User;
            user.Balance = dto.Balance;

            _unitOfWork.UserRepository.UpdateEntity(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserResponseDTO>> GetUsersInRange(int start, int count)
        {
            var users = await _unitOfWork.UserRepository.GetEntitiesInRangeAsync(start, count);

            if (users.Count == 0) throw new ArgumentException("Failed to find any users");

            return UserMapper.CreateDTOsFromEntities(users);
        }
        public async Task DeleteUser(string id)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id) ?? throw new ArgumentException($"Can`t find user by {id}");

            _unitOfWork.UserRepository.DeleteEntity(user);
        }
    }
}
