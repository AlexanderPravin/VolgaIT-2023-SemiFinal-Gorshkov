using App.VolgaIT.DTOs;
using App.VolgaIT.Mappers;
using Domain.VolgaIT.Entities;
using Infrastructure.VolgaIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.VolgaIT.Services
{
    public class TransportService
    {
        private readonly UnitOfWork _unitOfWork;

        public TransportService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TransportResponseDTO> GetTrasportByIdAsync(string id)
        {
            var transport = await _unitOfWork.TransportRepository.GetEntityByIdAsync(id) ?? throw new ArgumentException($"Can`t find trasport by {id}");

            return TransportMapper.CreateDTOFromEntity(transport);
        }

        public async Task<string> CreateTransport(TransportRequestDTO dto, string login)
        {
            var owner = await _unitOfWork.UserRepository.GetEntityByAsync(x => x.Login == login) ?? throw new ArgumentException($"Can`t find user by {login}");

            Transport transport = new()
            {
                Id = Guid.NewGuid(),
                CanBeRented = dto.CanBeRented,
                Color = dto.Color,
                DayPrice = dto.DayPrice,
                Description = dto.Description,
                Identifier = dto.Identifier,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                MinutePrice = dto.MinutePrice,
                Model = dto.Model,
                Owner = owner,
                TransportType = Enum.Parse<TransportType>(dto.TransportType)
            };

            _unitOfWork.TransportRepository.AddEntity(transport);
            await _unitOfWork.SaveChangesAsync();
            return transport.Id.ToString();
        }

        public async Task UpdateTransport(TransportRequestDTO dto, string id)
        {
            var transport = await _unitOfWork.TransportRepository.GetEntityByIdAsync(id) ?? throw new ArgumentException($"Can`t find transport by {id}");

            transport.TransportType = Enum.Parse<TransportType>(dto.TransportType);
            transport.DayPrice = dto.DayPrice;
            transport.MinutePrice = dto.MinutePrice;
            transport.CanBeRented = dto.CanBeRented;
            transport.Identifier = dto.Identifier ?? transport.Identifier;
            transport.Color = dto.Color ?? transport.Color;
            transport.Description = dto.Description;
            transport.Latitude = dto.Latitude;
            transport.Longitude = dto.Longitude;

            _unitOfWork.TransportRepository.UpdateEntity(transport);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTransport(string id)
        {
            var transport = await _unitOfWork.TransportRepository.GetEntityByIdAsync(id) ?? throw new ArgumentException($"Can`t find transport by {id}");

            _unitOfWork.TransportRepository.DeleteEntity(transport);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
