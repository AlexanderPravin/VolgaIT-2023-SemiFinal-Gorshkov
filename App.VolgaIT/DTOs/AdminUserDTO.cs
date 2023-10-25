

namespace App.VolgaIT.DTOs
{
    public class AdminUserDTO
    {
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsAdmin { get; set; }

        public double Balance { get; set; }
    }
}
