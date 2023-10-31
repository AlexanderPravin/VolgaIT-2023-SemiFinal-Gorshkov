using App.VolgaIT.DTOs;
using App.VolgaIT.Mappers;
using Domain.VolgaIT.Entities;
using Infrastructure.VolgaIT;

namespace App.VolgaIT.Services
{
    public class AdminTransportService
    {
        private readonly UnitOfWork _unitOfWork;
        
        public AdminTransportService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TransportResponseDTO>> GetTransportInRange(int start, int count, string transportType)
        {
            var transportList = await _unitOfWork.TransportRepository.GetEntitiesInRangeAsync(start, count);

            if(transportType !="All") transportList = transportList.Where(x=>x.TransportType.ToString() == transportType).ToList();

            var result = TransportMapper.CreateDTOsFromEntities(transportList);

            return result;
        }

        public async Task<TransportResponseDTO> GetTransportById(string id)
        {
            var transport = await _unitOfWork.TransportRepository.GetEntityByIdAsync(id) ?? throw new ArgumentException($"Can`t find transport by {id}");

            var result = TransportMapper.CreateDTOFromEntity(transport);

            return result;
        }

        public async Task<string> CreateTransportAsync(AdminTransportRequestDTO dto)
        {
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
                Owner = await _unitOfWork.UserRepository.GetEntityByIdAsync(dto.OwnerID) ?? throw new ArgumentException($"Can`t find owner by {dto.OwnerID}"),
                TransportType = Enum.Parse<TransportType>(dto.TransportType)
            };

            _unitOfWork.TransportRepository.AddEntity(transport);
            await _unitOfWork.SaveChangesAsync();
            return transport.Id.ToString();
        }

        public async Task UpdateTransport(AdminTransportRequestDTO dto, string id)
        {
            var transport = await _unitOfWork.TransportRepository.GetEntityByIdAsync(id) ?? throw new ArgumentException($"Can`t find transport by {id}");
            var owner = await _unitOfWork.UserRepository.GetEntityByIdAsync(dto.OwnerID) ?? throw new ArgumentException($"Can`t find user by {id}");

            transport.CanBeRented = dto.CanBeRented;
            transport.Color = dto.Color ?? transport.Color;
            transport.TransportType = Enum.Parse<TransportType>(dto.TransportType);
            transport.DayPrice = dto.DayPrice ?? transport.DayPrice;
            transport.Identifier = dto.Identifier ?? transport.Identifier;
            transport.Latitude = dto.Latitude;
            transport.Longitude = dto.Longitude;
            transport.Model = dto.Model ?? transport.Model;
            transport.Owner = owner;
            transport.Description = dto.Description ?? transport.Description;
            transport.MinutePrice = dto.MinutePrice ?? transport.MinutePrice;

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
