

namespace App.VolgaIT.DTOs
{
    public class AdminRentDTO
    {
        public long TransportID { get; set; }

        public long UserID { get; set; }

        public string TimeStart { get; set; } = null!;

        public string? TimeEnd { get; set; }

        public double PriceOfUnit { get; set; }

        public string PriceType { get; set; } = null!;

        public double? FinalPrice { get; set; }
    }
}
