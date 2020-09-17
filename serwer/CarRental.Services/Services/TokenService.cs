using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Token;
using System;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IRefreshRepository refreshRepository;
        private readonly IUserRepository userRepository;
        private readonly ITokenGeneratorService tokenGeneratorService;
        public TokenService(IRefreshRepository refreshRepository,IUserRepository userRepository,
                                                   ITokenGeneratorService tokenGeneratorService)
        {
            this.refreshRepository = refreshRepository;
            this.userRepository = userRepository;
            this.tokenGeneratorService = tokenGeneratorService;
        }
        /// <summary>
        /// Check date of end refresh token 
        /// delete old refresh token
        /// </summary>
        /// <param name="refresh"></param>
        /// <returns>return TokenClaimsDto with status checkRefreshToken true
        /// or with status checkRefreshToken false</returns>
        public async Task<TokenClaimsDto> CheckAccessRefreshTokenAsync(string refresh)
        {
            TokenClaimsDto token = new TokenClaimsDto();
            var check = await refreshRepository.FindByRefreshTokenAsync(refresh);
            if (check == null || check.DateOfEnd < DateTime.UtcNow)
            {
                token.CheckRefreshToken = false;
                return token;
            }
            check.Delete(false);
            await refreshRepository.SaveChangesAsync();
            token.UserId = check.UserId;
            token.CheckRefreshToken = true;
            return token;
        }
        /// <summary>
        /// Generate Access Token and Refresh Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>return TokenDto with code 200</returns>
        public async Task<TokenDto> GenerateRefreshTokenAsync(TokenClaimsDto token)
        {
            TokenDto tokenDto = new TokenDto();
            var user = await userRepository.FindByIdDetailsAsync(token.UserId);
            tokenDto.AccessToken = tokenGeneratorService.GenerateToken(user);
            tokenDto.RefreshToken = tokenGeneratorService.RefreshGenerateToken();
            tokenDto.Code = 200;
            return tokenDto;
        }
        /// <summary>
        /// Save Refresh token to database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="refreshtoken"></param>
        /// <param name="isvalid"></param>
        /// <returns>return model TokenDto</returns>
        public async Task<TokenDto> SaveRefreshTokenAsync(int id, string refreshtoken, bool isvalid)
        {
            RefreshToken refresh = new RefreshToken(refreshtoken, id, isvalid);
            refreshRepository.Create(refresh);
           await refreshRepository.SaveChangesAsync();
            return new TokenDto() { RefreshToken = refresh.Refresh };
        }
    }
}
