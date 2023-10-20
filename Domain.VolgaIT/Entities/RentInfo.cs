using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Guid TransportId { get; set; } 

        public Guid UserId { get; set; }

        [Required]
        private DateTime timeStart;

        private DateTime? timeEnd;

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
        
        public PriceType PriceType { get; set; }

        public double? FinalPrice { get; set; }

        [Required(ErrorMessage ="Transport is required")]
        public Transport? Transport { get; set; }

        [Required(ErrorMessage ="User is required")]
        public User? User { get; set; }

        [Required(ErrorMessage ="Owner is required")]
        public User? Owner { get; set; }    
    }
}
