

namespace App.VolgaIT.DTOs
{
    public class AdminTransportRequestDTO
    {
        public string OwnerID {  get; set; }

        public bool CanBeRented { get; set; }

        public string TransportType { get; set; } = default!;

        public string Model { get; set; } = default!;

        public string Color { get; set; } = default!;

        public string Identifier { get; set; } = default!;

        public string? Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double? MinutePrice { get; set; }

        public double? DayPrice { get; set;}
    }
}
