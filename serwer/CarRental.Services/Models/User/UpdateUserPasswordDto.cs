namespace CarRental.Services.Models.User
{
    public class UpdateUserPasswordDto
    {
        public string EncodePassword { get; set; }
        public string ConfirmEncodePassword { get; set; }
        public string CodeOfVerification { get; set; }

    }
}
