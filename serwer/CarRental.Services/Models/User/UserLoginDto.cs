namespace CarRental.Services.Models.User
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleOfWorker { get; set; }
    }
}
