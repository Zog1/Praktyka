using CarRental.DAL.Entities;

namespace CarRental.Services.Interfaces
{
    public interface ITokenGeneratorService
    {
         string GenerateToken(User user);
         string RefreshGenerateToken();
    }
}
