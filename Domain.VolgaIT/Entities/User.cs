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
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;

        private int balance = 0;

        public int Balance 
        {
            get { return balance; }
            set
            {
                if (value < 0) throw new Exception("Balance should be higher then zero");
                balance = value;
            }
        }
        private UserRole role;

        [Required]
        public string Role 
        {
            get
            {
                return role.ToString() ;
            } 
            set
            {
                if (!Enum.TryParse(value, true, out role)) throw new Exception("Not valid role recived");
            }
        }
        
        public ICollection<RentInfo> RentHistory { get; set; } = new List<RentInfo>();

        public ICollection<Transport> OwnedTransport { get; set; } = new List<Transport>();
    }
}
