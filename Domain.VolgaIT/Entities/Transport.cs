using System.ComponentModel.DataAnnotations;


namespace Domain.VolgaIT.Entities
{
    public enum TransportType
    {
        Car = 0,
        Bike = 1,
        Scooter = 2
    }
    public class Transport
    {
        public Guid Id { get; set; }

        // Indicates is this transport can be rented
        [Required]
        public bool CanBeRented { get; set; } = false;

        // Shows whats transport is this
        private TransportType transportType;

        public string TransportType
        {
            get { return transportType.ToString(); }
            set 
            {
                if (!Enum.TryParse(value.Trim(), true, out transportType)) 
                    throw new Exception("Incorrect transport type");
            }
        }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; } = null!;

        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; } = null!;

        [Required(ErrorMessage = "Identifier is required")]
        public string Identifier { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        private double? minutePrice;

        public double? MinutePrice
        {
            get
            {
                return minutePrice;
            }
            set
            {
                if (CanBeRented) minutePrice = value;
                else
                {
                    minutePrice = null;

                    throw new Exception("Can`t set price, because transport can`t be rent");
                }
            }
        }

        private double? dayPrice;

        public double? DayPrice
        {
            get
            {
                return dayPrice;
            }
            set
            {
                if (CanBeRented) dayPrice = value;
                else 
                {
                    dayPrice = null;

                    throw new Exception("Can`t set price, because transport can`t be rent");
                }

            }
        }

        [Required(ErrorMessage = "Owner is required")]
        public User Owner { get; set; } = null!;

        public bool IsRentedNow { get; set; } = false;

        //Information about rent history
        public ICollection<RentInfo> RentHistory { get; set; } = new List<RentInfo>();

    }
}
