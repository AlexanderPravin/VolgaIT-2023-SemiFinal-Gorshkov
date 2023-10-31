
namespace App.VolgaIT.DTOs
{
    public class AdminRentRequestDTO
    {
        public string TransportID { get; set; }

        public string UserID { get; set; }

        public string TimeStart { get; set; } = default!;

        public string? TimeEnd { get; set; }

        public double PriceOfUnit { get; set; }

        public string PriceType { get; set; } = default!;

        public double? FinalPrice { get; set; }
    }
}
