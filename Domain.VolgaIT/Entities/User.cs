using System.ComponentModel.DataAnnotations;


namespace Domain.VolgaIT.Entities
{
    public enum UserRole
    {
        User = 10,
        Admin = 20
    }
    public class User
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; } = default!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = default!;

        [Range(0, int.MaxValue)]
        public double Balance { get; set; } = 0;

        [Required]
        [EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; } = UserRole.User;
        
        public ICollection<RentInfo> RentHistory { get; set; } = new List<RentInfo>();

        public ICollection<Transport> OwnedTransport { get; set; } = new List<Transport>();
    }
}
