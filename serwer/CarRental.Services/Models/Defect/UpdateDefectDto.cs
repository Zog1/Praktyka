using CarRental.DAL.Entities;

namespace CarRental.Services.Models.Defect
{
    public class UpdateDefectDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }

    }
}
