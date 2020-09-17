using CarRental.Services.Models.Token;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface ITokenService
    {
        Task<TokenClaimsDto> CheckAccessRefreshTokenAsync(string refresh);
        Task<TokenDto> GenerateRefreshTokenAsync(TokenClaimsDto token);
        Task<TokenDto> SaveRefreshTokenAsync(int id, string refreshtoken, bool isvalid);

    }
}
