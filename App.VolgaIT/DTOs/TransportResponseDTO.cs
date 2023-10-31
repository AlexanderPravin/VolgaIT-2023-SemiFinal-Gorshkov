using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.VolgaIT.DTOs
{
    public class TransportResponseDTO
    {
        public string Id { get; set; }

        public bool CanBeRented { get; set; }

        public string TransportType { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public string Identifier { get; set; }

        public string? Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double? MinutePrice { get; set; }

        public double? DayPrice { get; set; }

        public bool IsRentedNow { get; set; }

        public string OwnerId { get; set; }
    }
}
