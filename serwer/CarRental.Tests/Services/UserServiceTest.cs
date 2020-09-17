using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Models.User;
using CarRental.Services.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Tests.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> mockUsersRepository;
        private readonly IMapper mapper;
        public UserServiceTest()
        {
            this.mockUsersRepository = new Mock<IUserRepository>();
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<User, UsersDto>();
            });
            mapper = config.CreateMapper();
        }
        [Fact]
        public async Task GetAllUsers_ReturnAllUser()
        {
            //Arrange
            List<User> users = new List<User> { new User(), new User() };
            this.mockUsersRepository
                .Setup(p => p.FindAllUsersAsync())
                .ReturnsAsync(users);
            var services = new UsersService(this.mockUsersRepository.Object, mapper);
            //Act
            var result = await services.GetAllUsersAsync();
            //Assert
            var assertResult = Assert.IsType<List<UsersDto>>(result);
            Assert.Equal(users.Count, assertResult.Count);
        }

        [Fact]
        public async Task GetUserById_ExcitingId_ReturnCorrectObject()
        {
            //Arrange
            int id = 1;
            this.mockUsersRepository
                .Setup(p => p.FindByIdDetailsAsync(id))
                .ReturnsAsync(new User { UserId = 1 });
            var services = new UsersService(this.mockUsersRepository.Object, mapper);
            //Act
            var result = await services.GetUserAsync(id);
            //Assert
            var assertResult = Assert.IsType<UsersDto>(result);
            Assert.Equal(id, assertResult.UserId);
        }

        [Fact]
        public async Task GetUserById_NotExcitingId_ReturnsNull()
        {
            //Arrange
            int id = 1;
            this.mockUsersRepository
               .Setup(p => p.FindByIdDetailsAsync(id))
                .ReturnsAsync((User)null);
            var services = new UsersService(this.mockUsersRepository.Object, mapper);
            //Act
            var result = await services.GetUserAsync(id);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUser_CorrectObject_ReturnObject()
        {
            //Arrange
            UsersDto usersDto = new UsersDto()
            {
                UserId = 1,
                FirstName = "Bohdan",
                Email = "kucher@gmail.com",
                LastName = "Kucher",
                MobileNumber = "123123123",
                IdentificationNumber = "123123"
            };
            User user = new User()
            {
                UserId = 1,
                FirstName = "Bogdan",
                Email = "kucher@gmail.com",
                LastName = "Kuchera",
                MobileNumber = "444444444",
                IdentificationNumber = "123123"
            };
            this.mockUsersRepository
               .Setup(p => p.FindByIdDetailsAsync(1))
                .ReturnsAsync(user);
            this.mockUsersRepository
               .Setup(u => u.Update(user))
                .Verifiable();
            this.mockUsersRepository
               .Setup(u => u.SaveChangesAsync())
                .Verifiable();
            var services = new UsersService(this.mockUsersRepository.Object, mapper);
            //Act
            var result = await services.UpdateUserAsync(usersDto);
            Assert.Equal(result.UserId, usersDto.UserId);
            Assert.Equal(result.FirstName, usersDto.FirstName);
            Assert.Equal(result.LastName, usersDto.LastName);
            Assert.Equal(result.MobileNumber, usersDto.MobileNumber);
            Assert.Equal(result.IdentificationNumber, usersDto.IdentificationNumber);
        }

        [Fact]
        public async Task UpdateUser_UserNotFound_ReturnObject()
        {
            //Arrange
            UsersDto usersDto = new UsersDto()
            {
                UserId = 1,
                FirstName = "Bohdan",
                Email = "kucher@gmail.com",
                LastName = "Kucher",
                MobileNumber = "123123123",
                IdentificationNumber = "123123"
            };
            this.mockUsersRepository
               .Setup(p => p.FindByIdDetailsAsync(usersDto.UserId))
                .ReturnsAsync(null as User);
            var services = new UsersService(this.mockUsersRepository.Object, mapper);
            //Act
            var result = await services.UpdateUserAsync(usersDto);
            //Assert
            Assert.False(result.isValid);
        }

        [Fact]
        public async Task DeleteUser_IdExiciting_ReturnTrue()
        {
            //Arrange
            int id = 2;
            this.mockUsersRepository
               .Setup(p => p.FindByIdAsync(id))
                .ReturnsAsync(new User()
                {
                    UserId = 2,
                    FirstName = "Bohdan"
                });
            this.mockUsersRepository
             .Setup(s => s.SaveChangesAsync())
              .Verifiable();
            var services = new UsersService(this.mockUsersRepository.Object, mapper);
            //Act
            var result = await services.DeleteUserAsync(id);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUser_IdNotFound_ReturnTrue()
        {
            //Arrange
            int id = 2;
            this.mockUsersRepository
               .Setup(p => p.FindByIdAsync(id))
                .ReturnsAsync(null as User);
            var services = new UsersService(this.mockUsersRepository.Object, mapper);
            //Act
            var result = await services.DeleteUserAsync(id);
            //Assert
            Assert.False(result);
        }
    }
}
