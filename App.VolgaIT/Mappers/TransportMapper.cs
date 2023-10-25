using App.VolgaIT.DTOs;
using Domain.VolgaIT.Entities;
using Domain.VolgaIT.Interfaces;

namespace App.VolgaIT.Mappers
{
    public class TransportMapper : IMapper<Transport, TransportDTO>
    {

        public static TransportDTO CreateDTOFromEntity(Transport transport)
        {
            return new TransportDTO
            {
                CanBeRented = transport.CanBeRented,
                TransportType = transport.TransportType,
                Model = transport.Model,
                Color = transport.Color,
                Identifier = transport.Identifier,
                Description = transport.Description,
                Latitude = transport.Latitude,
                Longitude = transport.Longitude,
                MinutePrice = transport.MinutePrice,
                DayPrice = transport.DayPrice,
            };
        }

        public static IEnumerable<TransportDTO> CreateDTOsFromEntities(IEnumerable<Transport> transports)
        {
            var TransportDTOList = new List<TransportDTO>();

            foreach (var transport in transports)
            {
                TransportDTOList.Add(CreateDTOFromEntity(transport));
            }

            return TransportDTOList;
        }
    }
}
