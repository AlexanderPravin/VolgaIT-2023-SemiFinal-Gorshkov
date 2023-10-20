using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.VolgaIT.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Login is required")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required]
        public int Balance { get; set; } = 0;

        public ICollection<RentInfo> RentHistory { get; set; } = new Collection<RentInfo>();

        public ICollection<Transport> OwnedTransport { get;} = new Collection<Transport>();
    }
}
