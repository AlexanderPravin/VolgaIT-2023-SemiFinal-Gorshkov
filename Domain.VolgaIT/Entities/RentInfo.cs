using System.ComponentModel.DataAnnotations;


namespace Domain.VolgaIT.Entities
{
    public enum PriceType
    {
        Minutes = 0,
        Days = 1
    }

    public class RentInfo
    {
        public Guid Id { get; set; }

        private DateTime timeStart;

        private DateTime? timeEnd;

        [Required]
        public string TimeStart
        {
            get
            {
                return timeStart.ToString("O");
            }
            set
            {
                timeStart = DateTime.Parse(value, null, System.Globalization.DateTimeStyles.RoundtripKind);
            }
        }

        public string? TimeEnd
        {
            get
            {
                return timeEnd?.ToString("O");
            }
            set
            {
                if (value == null) timeEnd = null;
                else timeEnd = DateTime.Parse(value, null, System.Globalization.DateTimeStyles.RoundtripKind);
            }
        }

        [Required]
        public double PriceOfUnit { get; set; }

        private PriceType priceType;

        [Required]
        public string PriceType
        {
            get { return priceType.ToString(); }
            set
            {
                if (!Enum.TryParse(value, true, out priceType)) throw new Exception("Incorrect price type");
            }
        }

        public double? FinalPrice { get; set; }

        [Required(ErrorMessage = "Transport is required")]
        public Transport Transport { get; set; } = null!;

        [Required]
        public Guid TransportId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "User is required")]
        public User CurrentUser { get; set; } = null!;

        [Required(ErrorMessage = "Owner is required")]
        public User Owner { get; set; } = null!;    
    }
}
