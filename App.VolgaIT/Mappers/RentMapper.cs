using App.VolgaIT.DTOs;
using Domain.VolgaIT.Entities;
using Domain.VolgaIT.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.VolgaIT.Mappers
{
    public class RentMapper : IMapper<RentInfo, RentResponseDTO>
    {
        public static RentResponseDTO CreateDTOFromEntity(RentInfo entity)
        {
            return new RentResponseDTO
            {
                Id = entity.Id.ToString(),
                TransportId = entity.TransportId.ToString(),
                UserId = entity.UserId.ToString(),
                FinalPrice = entity.FinalPrice,
                PriceOfUnit = entity.PriceOfUnit,
                PriceType = entity.PriceType.ToString(),
                TimeEnd = entity.PriceType.ToString("0"),
                TimeStart = entity.PriceType.ToString("0"),
                OwnerId = entity.OwnerId.ToString(),
            };
        }

        public static IEnumerable<RentResponseDTO> CreateDTOsFromEntities(IEnumerable<RentInfo> entities)
        {
            var rentResponseList = new List<RentResponseDTO>();
            foreach(var entity in entities)
            {
                rentResponseList.Add(CreateDTOFromEntity(entity));
            }
            return rentResponseList;
        }
    }
}
