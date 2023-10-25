﻿
namespace App.VolgaIT.DTOs
{
    public class TransportDTO
    {
        public bool CanBeRented { get; set; }

        public string TransportType { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string Color { get; set; } = null!;

        public string Identifier { get; set; } = null!;

        public string? Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
        
        public double? MinutePrice { get; set; }

        public double? DayPrice { get; set; }
    }
}
