using App.VolgaIT.DTOs;
using Domain.VolgaIT.Entities;
using Domain.VolgaIT.Interfaces;

namespace App.VolgaIT.Mappers
{
    public class TransportMapper : IMapper<Transport, TransportResponseDTO>
    {

        public static TransportResponseDTO CreateDTOFromEntity(Transport transport)
        {
            return new TransportResponseDTO
            { 
                Id = transport.Id.ToString(),
                CanBeRented = transport.CanBeRented,
                TransportType = transport.TransportType.ToString(),
                Model = transport.Model,
                Color = transport.Color,
                Identifier = transport.Identifier,
                Description = transport.Description,
                Latitude = transport.Latitude,
                Longitude = transport.Longitude,
                MinutePrice = transport.MinutePrice,
                DayPrice = transport.DayPrice,
                IsRentedNow = transport.IsRentedNow,
                OwnerId = transport.Owner.Id.ToString()
            };
        }

        public static IEnumerable<TransportResponseDTO> CreateDTOsFromEntities(IEnumerable<Transport> transports)
        {
            var TransportDTOList = new List<TransportResponseDTO>();

            foreach (var transport in transports)
            {
                TransportDTOList.Add(CreateDTOFromEntity(transport));
            }

            return TransportDTOList;
        }
    }
}
