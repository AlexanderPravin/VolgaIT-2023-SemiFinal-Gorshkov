

namespace App.VolgaIT.DTOs
{
    public class AdminUserRequestDTO
    {
        public string UserName { get; set; } = default!;

        public string Password { get; set; } = default!;

        public bool IsAdmin { get; set; }

        public double Balance { get; set; }
    }
}
