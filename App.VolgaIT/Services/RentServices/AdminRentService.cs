using App.VolgaIT.DTOs;
using App.VolgaIT.Mappers;
using Domain.VolgaIT.Entities;
using Infrastructure.VolgaIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.VolgaIT.Services.RentServices
{
    public class AdminRentService
    {
        private readonly UnitOfWork _unitOfWork;
        
        public AdminRentService(UnitOfWork unitOfWork)
        {  
            _unitOfWork = unitOfWork; 
        }

        public async Task<RentResponseDTO> GetRentInfo(string id)
        {
            var rent = await _unitOfWork.RentRepository.GetEntityByIdAsync(id) 
                ?? throw new Exception();

            return RentMapper.CreateDTOFromEntity(rent);
        }

        public async Task<IEnumerable<RentResponseDTO>> GetRentHistoryForUser(string id)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id) 
                ?? throw new Exception();

            return RentMapper.CreateDTOsFromEntities(user.RentHistory);
        }

        public async Task<IEnumerable<RentResponseDTO>> GetRentHistoryForTransport(string id)
        {
            var transport = await _unitOfWork.TransportRepository.GetEntityByIdAsync(id) 
                ?? throw new Exception();

            return RentMapper.CreateDTOsFromEntities(transport.RentHistory);
        }

        public async Task<string> CreateNewRent(AdminRentRequestDTO requestDTO)
        {
            var transport = await _unitOfWork.TransportRepository.GetEntityByIdAsync(requestDTO.TransportID) 
                ?? throw new Exception();

            var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(requestDTO.UserID) 
                ?? throw new Exception();

            RentInfo rent = new()
            {
                TransportId = transport.Id,
                IsActive = true,
                FinalPrice = requestDTO.FinalPrice,
                Id = Guid.NewGuid(),
                User = user,
                UserId = user.Id,
                Owner = transport.Owner,
                OwnerId = transport.OwnerId,
                PriceOfUnit = requestDTO.PriceOfUnit,
                PriceType = Enum.Parse<PriceType>(requestDTO.PriceType),
                TimeStart = DateTime.Parse(requestDTO.TimeStart),
                Transport = transport,
            };
            if (requestDTO.TimeEnd == null) rent.TimeEnd = null;
            else rent.TimeEnd = DateTime.Parse(requestDTO.TimeEnd);

            transport.RentHistory.Add(rent);
            user.RentHistory.Add(rent);
            _unitOfWork.UserRepository.UpdateEntity(user);
            _unitOfWork.TransportRepository.UpdateEntity(transport);
            _unitOfWork.RentRepository.AddEntity(rent);
            await _unitOfWork.SaveChangesAsync();

            return rent.Id.ToString();
        }

        public async Task EndRent(string id, double latitude, double longtitude)
        {
            var rent = await _unitOfWork.RentRepository.GetEntityByIdAsync(id) 
                ?? throw new NotImplementedException();

            if(!rent.IsActive) throw new NotImplementedException();

            var transport = await _unitOfWork.TransportRepository.GetEntityByIdAsync(rent.Id.ToString()) 
                ?? throw new NotImplementedException();

            transport.Longitude = longtitude;
            transport.Latitude = latitude;

            rent.TimeEnd = DateTime.Now;
            switch (rent.PriceType)
            {
                case PriceType.Days:
                    rent.FinalPrice = rent.PriceOfUnit * (rent.TimeEnd - rent.TimeStart).Value.TotalDays;
                    break;
                case PriceType.Minutes:
                    rent.FinalPrice = rent.PriceOfUnit * (rent.TimeEnd - rent.TimeStart).Value.TotalMinutes;
                    break;
            }
            rent.IsActive = false;

            _unitOfWork.RentRepository.UpdateEntity(rent);
            _unitOfWork.TransportRepository.UpdateEntity(transport);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateRent(string id, AdminRentRequestDTO dto)
        {
            var rent = await _unitOfWork.RentRepository.GetEntityByIdAsync(id) 
                ?? throw new NotImplementedException();

            var transport = await _unitOfWork.TransportRepository.GetEntityByIdAsync(id) 
                ?? throw new NotImplementedException();

            var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(id) 
                ?? throw new Exception();

            rent.PriceOfUnit = dto.PriceOfUnit;
            rent.FinalPrice = dto.FinalPrice;
            rent.Transport = transport;
            rent.TransportId = Guid.Parse(dto.TransportID);
            rent.TimeStart = DateTime.Parse(dto.TimeStart);
            rent.UserId = Guid.Parse(dto.UserID);
            rent.PriceType = Enum.Parse<PriceType>(dto.PriceType);
            rent.User = user;
            if (dto.TimeEnd == null) rent.TimeEnd = null;
            else rent.TimeEnd = DateTime.Parse(dto.TimeEnd);

            _unitOfWork.RentRepository.UpdateEntity(rent);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteRent(string id)
        {
            var rent = await _unitOfWork.RentRepository.GetEntityByIdAsync(id) 
                ?? throw new NotImplementedException();

            _unitOfWork.RentRepository.DeleteEntity(rent);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
