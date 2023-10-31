using App.VolgaIT.DTOs;
using App.VolgaIT.Helpers;
using App.VolgaIT.Mappers;
using Domain.VolgaIT.Entities;
using Domain.VolgaIT.Options;
using Domain.VolgaIT.Settings;
using Infrastructure.VolgaIT;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace App.VolgaIT.Services
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly JWTHelper _jwtHelper;
        private readonly PasswordHelper _passwordHelper;

        public UserService(UnitOfWork unitOfWork, JwtOptions jwtOptions, PasswordHashOptions passwordHashOptions)
        {
            _unitOfWork = unitOfWork;
            _passwordHelper = new PasswordHelper(passwordHashOptions);
            _jwtHelper = new JWTHelper(jwtOptions);
        }

        public async Task RegisterAsync(UserRequestDTO dto)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByAsync(x => x.Login == dto.UserName);
            if (user is not null)
                throw new ArgumentException("Login should be unique ");

            user = new()
            {
                Id = Guid.NewGuid(),
                Login = dto.UserName,
                Password = _passwordHelper.GetPassword(dto.Password),
                Role = UserRole.User,
            };

            _unitOfWork.UserRepository.AddEntity(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<string> SignInAsync(UserRequestDTO signDTO)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByAsync(x => x.Login == signDTO.UserName) ?? throw new ArgumentException("User not found");

            if (!_passwordHelper.CheckPasswords(user.Password, signDTO.Password))
                throw new ArgumentException("Incorrect password");

            return _jwtHelper.GetJwtToken(user);
        }

        public async Task UpdateUserAsync(UserRequestDTO dto, string login)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByAsync(x => x.Login == dto.UserName);
            if (user is not null)
                throw new ArgumentException("Login already used");

            user = await _unitOfWork.UserRepository.GetEntityByAsync(x => x.Login == login) ?? throw new ArgumentException($"Can`t find user by {login}");

            user.Login = dto.UserName;
            user.Password = dto.Password;

            _unitOfWork.UserRepository.UpdateEntity(user);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserResponseDTO> GetUserAsync(string login)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByAsync(x => x.Login == login) ?? throw new ArgumentException($"Can`t find user by {login}");

            var dto = UserMapper.CreateDTOFromEntity(user);

            return dto;
        }

    }
}
