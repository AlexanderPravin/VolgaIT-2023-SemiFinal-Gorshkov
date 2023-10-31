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

        [Required]
        public bool CanBeRented { get; set; } = false;

        [EnumDataType(typeof(TransportType))]
        public TransportType TransportType { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; } = default!;

        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; } = default!;

        [Required(ErrorMessage = "Identifier is required")]
        public string Identifier { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Range(0, double.MaxValue)]
        public double? MinutePrice { get; set; }

        [Range(0, double.MaxValue)]
        public double? DayPrice { get; set; }
        [Required]
        public Guid OwnerId { get; set; }

        [Required(ErrorMessage = "Owner is required")]
        public User Owner { get; set; } = default!;

        public bool IsRentedNow { get; set; } = false;

        public ICollection<RentInfo> RentHistory { get; set; } = new List<RentInfo>();

    }
}
