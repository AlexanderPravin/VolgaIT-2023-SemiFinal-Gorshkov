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
        public bool IsActive { get; set; }

        [Required]
        public DateTime TimeStart { get; set; }

        public DateTime? TimeEnd { get; set; }

        [Required, Range(0, double.MaxValue)]
        public double PriceOfUnit { get; set; }

        [Required]
        [EnumDataType(typeof(PriceType))]
        public PriceType PriceType { get;set; }

        [Range(0, double.MaxValue)]
        public double? FinalPrice { get; set; }

        [Required(ErrorMessage = "Transport is required")]
        public Transport Transport { get; set; } = default!;

        [Required]
        public Guid TransportId { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid OwnerId { get;set; }
        [Required]
        public User Owner { get; set; }
        [Required(ErrorMessage = "User is required")]
        public User User { get; set; } = default!; 
    }
}
