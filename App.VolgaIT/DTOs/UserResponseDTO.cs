using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.VolgaIT.DTOs
{
    public class UserResponseDTO
    {
        public string Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public double Balance { get; set; }

        public string Role { get; set; }
    }
}
