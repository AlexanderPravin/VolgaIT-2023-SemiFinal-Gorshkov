using App.VolgaIT.DTOs;
using App.VolgaIT.Mappers;
using Domain.VolgaIT.Entities;
using Infrastructure.VolgaIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.VolgaIT.Services.RentServices
{
    public class RentService
    {
        private readonly UnitOfWork _unitOfWork;
        public RentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<TransportResponseDTO>> GetTransportToRent(double latitude, double longtitude, double radius, string type)
        {
            var transportList = await _unitOfWork.TransportRepository.GetEntitiesByAsync(x => Math.Pow(x.Latitude - latitude, 2) + Math.Pow(x.Longitude - longtitude, 2) <= Math.Pow(radius,2));
            if (type != "All")
                transportList = transportList.Where(x => x.TransportType.ToString() == type).ToList();

            return TransportMapper.CreateDTOsFromEntities(transportList);
        }
        public async Task<RentResponseDTO> GetRentInfoById(string id, string login)
        {
            var rent = await _unitOfWork.RentRepository.GetEntityByIdAsync(id) 
                ?? throw new ArgumentException($"Can`t find rent by {id}");

            var user = await _unitOfWork.UserRepository.GetEntityByIdAsync(rent.UserId.ToString()) 
                ?? throw new ArgumentException("Can`t find user");

            var owner = await _unitOfWork.UserRepository.GetEntityByIdAsync(rent.OwnerId.ToString())
                ?? throw new ArgumentException("Can`t find user");

            if (user.Login != login || owner.Login != login)
                throw new ArgumentException("You don`t have access for this RentInfo");

            return RentMapper.CreateDTOFromEntity(rent);
        }

        public async Task<IEnumerable<RentResponseDTO>> GetRentInfoForUser(string login)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByAsync(x => x.Login == login) 
                ?? throw new ArgumentException($"Can`t find user by {login}");

            return RentMapper.CreateDTOsFromEntities(user.RentHistory);
        }

        public async Task<IEnumerable<RentResponseDTO>> GetRentInfoForTransport(string id, string login)
        {
            var user = await _unitOfWork.UserRepository.GetEntityByAsync(x => x.Login == login) 
                ?? throw new ArgumentException($"Can`t find user by {login}");

            var transport = await _unitOfWork.TransportRepository.GetEntityByIdAsync(id) 
                ?? throw new ArgumentException($"Can`t find transport by {id}");

            if (user.Id != transport.Owner.Id) throw new ArgumentException("You don`t have access for this transport history");

            return RentMapper.CreateDTOsFromEntities(transport.RentHistory);
        }

        public async Task<string> RentTransport(string id, string rentType, string login)
        {
            var transport = await _unitOfWork.TransportRepository.GetEntityByIdAsync(id) 
                ?? throw new ArgumentException($"Can`t find transport by {id}");

            var user = await _unitOfWork.UserRepository.GetEntityByAsync(x => x.Login == login) 
                ?? throw new ArgumentException($"Can`t find user by {login}");

            if (transport.Owner.Id == user.Id)
                throw new ArgumentException("Can`t rent your own transport");

            if (transport.IsRentedNow || !transport.CanBeRented) 
                throw new ArgumentException("Transport can`t be rented");

            RentInfo rentInfo = new()
            {
                Id = Guid.NewGuid(),
                TransportId = transport.Id,
                User = user,
                IsActive = true,
                Transport = transport,
                PriceType = Enum.Parse<PriceType>(rentType),
                TimeStart = DateTime.Now,
                UserId = user.Id,
                Owner = transport.Owner,
                OwnerId = transport.OwnerId
            };

            switch(rentInfo.PriceType)
            {
                case PriceType.Days: rentInfo.PriceOfUnit = transport.DayPrice ?? throw new ArgumentException("Transport doesn`t have price for day"); 
                    break;
                case PriceType.Minutes: rentInfo.PriceOfUnit = transport.MinutePrice ?? throw new ArgumentException("Transport doesn`t have price for minutes");
                    break;
                default: throw new NotImplementedException();
            }

            transport.IsRentedNow = true;
            transport.RentHistory.Add(rentInfo);
            user.RentHistory.Add(rentInfo);

            _unitOfWork.UserRepository.UpdateEntity(user);
            _unitOfWork.TransportRepository.UpdateEntity(transport);
            _unitOfWork.RentRepository.AddEntity(rentInfo);

            await _unitOfWork.SaveChangesAsync();
            return rentInfo.Id.ToString();
        }

        public async Task EndRent(double latitude, double longitude, string id)
        {
            var rent = await _unitOfWork.RentRepository.GetEntityByIdAsync(id) ?? throw new ArgumentException($"Can`t find rent by {id}");

            if (!rent.IsActive) throw new ArgumentException("Rent already ended");

            rent.IsActive = false;
            
            rent.TimeEnd = DateTime.Now;

            switch(rent.PriceType)
            {
                case PriceType.Days: rent.FinalPrice = rent.PriceOfUnit * (rent.TimeEnd - rent.TimeStart).Value.TotalDays; 
                    break;
                case PriceType.Minutes: rent.FinalPrice = rent.PriceOfUnit * (rent.TimeEnd - rent.TimeStart).Value.TotalMinutes; 
                    break;
            }

            rent.Transport.Latitude = latitude;
            rent.Transport.Longitude = longitude;

            _unitOfWork.TransportRepository.UpdateEntity(rent.Transport);
            _unitOfWork.RentRepository.UpdateEntity(rent);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
