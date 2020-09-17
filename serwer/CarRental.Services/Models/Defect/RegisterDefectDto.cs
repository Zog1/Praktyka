namespace CarRental.Services.Models.Defect
{
    public class RegisterDefectDto
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
        public string Description { get; set; }
    }
}
