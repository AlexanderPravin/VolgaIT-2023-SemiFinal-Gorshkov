using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool CanBeRented { get; set; }

        // Shows whats transport is this
        [Required]
        public TransportType TransportType { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string? Model { get; set; }

        [Required(ErrorMessage = "Color is required")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Identifier is required")]
        public string? Identifier { get; set; }

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
                else minutePrice = null;
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
                else dayPrice = null;
            }
        }

        [Required(ErrorMessage = "Owner is required")]
        public User? Owner { get; set; }

        public bool IsRentedNow { get; set; } = false;

        //Information about current rent, could be null
        public RentInfo? CurrentRent { get; set; }

        //Information about rent history
        public ICollection<RentInfo> RentHistory { get; set; } = new List<RentInfo>();

    }
}
