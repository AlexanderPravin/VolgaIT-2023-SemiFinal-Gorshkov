
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace App.VolgaIT.DTOs
{
    public class RentResponseDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TransportId { get; set; }
        public string OwnerId { get; set; }
        public string TimeStart { get; set; }
        public string? TimeEnd { get; set; } 
        public string PriceType { get; set; }
        public double PriceOfUnit { get; set; }
        public double? FinalPrice { get; set; }
    }
}   
