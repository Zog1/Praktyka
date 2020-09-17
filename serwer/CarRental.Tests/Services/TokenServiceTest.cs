using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Models.Token;
using CarRental.Services.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Tests.Services
{
    public class TokenServiceTest
    {
        private readonly Mock<IRefreshRepository> mockRefreshRepository;
        private readonly Mock<IUserRepository> mockUsersRepository;
        public TokenServiceTest()
        {
            mockRefreshRepository = new Mock<IRefreshRepository>();
            mockUsersRepository = new Mock<IUserRepository>();
        }
        [Fact]
        public async Task CheckAccessRefreshToken_RefreshTokenBad_ReturnFalse()
        {
            //Arrange
            string refresh = "PeaIvTHLbjgGRd2lQAXhLK85TkGI4En9GJ7mHThT8DmyzWA/C4y8aOKqqYXUqxL3qxdPih2WhB2YQ2tqZZJG7hDavGdLgHoW8ZGAVc84MqdDTaDOr0gHZVJgvPyp5Wl7FPZP7q3fh7MYRfTaTzMZSqooADcErb4CdgyEFrlAb8o=";
            mockRefreshRepository
                .Setup(p => p.FindByRefreshTokenAsync(refresh))
                .ReturnsAsync(null as RefreshToken);
            var tokenGeneratorService = new Mock<TokenGeneratorService>();
            var services = new TokenService(mockRefreshRepository.Object, mockUsersRepository.Object, tokenGeneratorService.Object);
            //Act
            var result = await services.CheckAccessRefreshTokenAsync(refresh);
            //Assert
            Assert.False(result.CheckRefreshToken);
        }

        [Fact]
        public async Task CheckAccessRefreshToken_RefreshTokenDateNotCorrect_ReturnFalse()
        {
            //Arrange
            string refresh = "PeaIvTHLbjgGRd2lQAXhLK85TkGI4En9GJ7mHThT8DmyzWA/C4y8aOKqqYXUqxL3qxdPih2WhB2YQ2tqZZJG7hDavGdLgHoW8ZGAVc84MqdDTaDOr0gHZVJgvPyp5Wl7FPZP7q3fh7MYRfTaTzMZSqooADcErb4CdgyEFrlAb8o=";
            mockRefreshRepository
                .Setup(p => p.FindByRefreshTokenAsync(refresh))
                .ReturnsAsync(new RefreshToken());
            var tokenGeneratorService = new Mock<TokenGeneratorService>();
            var services = new TokenService(mockRefreshRepository.Object, mockUsersRepository.Object, tokenGeneratorService.Object);
            //Act
            var result = await services.CheckAccessRefreshTokenAsync(refresh);
            //Assert
            Assert.False(result.CheckRefreshToken);
        }

        [Fact]
        public async Task CheckAccessRefreshToken_RefreshTokenCorrect_ReturnTrue()
        {
            //Arrange
            string refresh = "PeaIvTHLbjgGRd2lQAXhLK85TkGI4En9GJ7mHThT8DmyzWA/C4y8aOKqqYXUqxL3qxdPih2WhB2YQ2tqZZJG7hDavGdLgHoW8ZGAVc84MqdDTaDOr0gHZVJgvPyp5Wl7FPZP7q3fh7MYRfTaTzMZSqooADcErb4CdgyEFrlAb8o=";
            mockRefreshRepository
                .Setup(p => p.FindByRefreshTokenAsync(refresh))
                  .ReturnsAsync(new RefreshToken()
                  {
                      DateOfStart = DateTime.Now,
                      DateOfEnd = DateTime.Now.AddDays(20)
                  });
            mockRefreshRepository
                .Setup(p => p.SaveChangesAsync())
                .Verifiable();
            var tokenGeneratorService = new Mock<TokenGeneratorService>();
            var services = new TokenService(mockRefreshRepository.Object, mockUsersRepository.Object, tokenGeneratorService.Object);
            //Act
            var result = await services.CheckAccessRefreshTokenAsync(refresh);
            //Assert
            Assert.True(result.CheckRefreshToken);
        }
        [Fact]
        public async Task GenerateRefreshToken_CorrectModel_ReturnAccessRefreshToken()
        {
            TokenClaimsDto token = new TokenClaimsDto()
            {
                UserId = 2
            };
            mockUsersRepository
                .Setup(p => p.FindByIdDetailsAsync(token.UserId))
                .ReturnsAsync(new User()
                {
                    Email = "kucherbogdan@gmail.com",
                    HashPassword = "213123adfe",
                    UserId = 2,
                    RoleOfUser = RoleOfWorker.Worker
                });
            var tokenGeneratorService = new Mock<TokenGeneratorService>();
            var services = new TokenService(mockRefreshRepository.Object, mockUsersRepository.Object, tokenGeneratorService.Object);
            //Act
            var result = await services.GenerateRefreshTokenAsync(token);
            //Assert
            Assert.Equal(200, result.Code);
        }
        [Fact]
        public async Task SaveRefreshToken_CorrectSaveRefreshToken()
        {
            //Arrange
            var refresh = new RefreshToken()
            {
                Refresh = "PeaIvTHLbjgGRd2lQAXhLK85TkGI4En9GJ7mHThT8DmyzWA/C4y8aOKqqYXUqxL3qxdPih2WhB2YQ2tqZZJG7hDavGdLgHoW8ZGAVc84MqdDTaDOr0gHZVJgvPyp5Wl7FPZP7q3fh7MYRfTaTzMZSqooADcErb4CdgyEFrlAb8o=",
                UserId = 2,
                IsValid = true,
                DateOfStart = DateTime.Now,
                DateOfEnd = DateTime.Now.AddDays(100)
            };
            mockRefreshRepository
                .Setup(p=>p.Create(refresh))
                .Verifiable();
            mockRefreshRepository
                .Setup(p => p.SaveChangesAsync())
                .Verifiable();
            var tokenGeneratorService = new Mock<TokenGeneratorService>();
            var services = new TokenService(mockRefreshRepository.Object, mockUsersRepository.Object, tokenGeneratorService.Object);
            //Act
            var result  = await services.SaveRefreshTokenAsync(refresh.UserId, refresh.Refresh, refresh.IsValid);
            //Assert
            Assert.NotNull(result);
        }

    }
}
