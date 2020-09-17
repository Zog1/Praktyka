using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Models.Defect;
using CarRental.Services.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Tests.Services
{
    public class DefectServiceTest
    {
        private readonly Mock<IUserRepository> mockUsersRepository;
        private readonly Mock<ICarRepository> mockCarsRepository;
        private readonly Mock<IDefectRepository> mockDefectsRepository;
        private readonly IMapper mapper;
        public DefectServiceTest()
        {
            mockUsersRepository = new Mock<IUserRepository>();
            mockCarsRepository = new Mock<ICarRepository>();
            mockDefectsRepository = new Mock<IDefectRepository>();
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<Defect, DefectDto>();
            });
            mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetAllDefects_ExcitingObjects_ReturnAllDefects()
        {
            //Arrange
            List<Defect> defects = new List<Defect> { new Defect() };
            mockDefectsRepository
                .Setup(p => p.FindAllDefectsAsync())
                .ReturnsAsync(defects);
            var services = new DefectService(mockUsersRepository.Object, mockCarsRepository.Object, mapper, mockDefectsRepository.Object);
            //Act
            var result = await services.GetAllDefectsAsync();
            //Assert
            var assertResult = Assert.IsType<List<DefectDto>>(result);
            Assert.Equal(defects.Count, assertResult.Count);
        }

        [Fact]
        public async Task GetDefectAsync_ExcitingId_ReturnDefect()
        {
            //Arrange
            int DefectId = 1;
            mockDefectsRepository
                .Setup(p => p.FindDefectByIdAsync(DefectId))
                .ReturnsAsync(new Defect { DefectId = 1 });
            var services = new DefectService(mockUsersRepository.Object, mockCarsRepository.Object, mapper, mockDefectsRepository.Object);
            //Act
            var result = await services.GetDefectAsync(DefectId);
            //Assert
            var assertResult = Assert.IsType<DefectDto>(result);
            Assert.Equal(DefectId, assertResult.DefectId);
        }

        [Fact]
        public async Task GetDefectAsync_IdNotExciting_ReturnNullDefect()
        {
            //Arrange
            int DefectId = 1;
            mockDefectsRepository
                .Setup(p => p.FindDefectByIdAsync(DefectId))
                .ReturnsAsync(null as Defect);
            var services = new DefectService(mockUsersRepository.Object, mockCarsRepository.Object, mapper, mockDefectsRepository.Object);
            //Act
            var result = await services.GetDefectAsync(DefectId);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task RegisterDefect_ExcitingUserCar_ReturnDefect()
        {
            RegisterDefectDto registerDefectDto = new RegisterDefectDto()
            {
                UserId = 1,
                CarId = 1,
                Description = "Stop Working Window"
            };
            mockUsersRepository
                .Setup(p => p.FindByIdDetailsAsync(registerDefectDto.UserId))
                .ReturnsAsync(new User() { UserId = 1 });
            mockCarsRepository
                .Setup(c => c.FindByIdAsync(registerDefectDto.CarId))
                .ReturnsAsync(new Car() { CarId = 1 });
            mockDefectsRepository
                .Setup(d => d.Create(It.IsAny<Defect>()))
                .Verifiable();
            mockDefectsRepository
                .Setup(s => s.SaveChangesAsync())
                .Verifiable();
            var services = new DefectService(mockUsersRepository.Object, mockCarsRepository.Object, mapper, mockDefectsRepository.Object);
            //Act
            var result = await services.RegisterDefectAsync(registerDefectDto);
            //Assert
            var assertResult = Assert.IsType<DefectDto>(result);
            Assert.Equal(result.CarId, assertResult.CarId);
            Assert.Equal(result.UserId, assertResult.UserId);
            Assert.Equal(result.Description, assertResult.Description);
        }

        [Fact]
        public async Task RegisterDefect_UserIdCarIdNotExciting_ReturnNull()
        {
            RegisterDefectDto registerDefectDto = new RegisterDefectDto()
            {
                UserId = 1,
                CarId = 1,
                Description = "Stop Working Window"
            };
            mockUsersRepository
              .Setup(p => p.FindByIdDetailsAsync(registerDefectDto.UserId))
              .ReturnsAsync(null as User);
            mockCarsRepository
                .Setup(c => c.FindByIdAsync(registerDefectDto.CarId))
                .ReturnsAsync(null as Car);
            var services = new DefectService(mockUsersRepository.Object, mockCarsRepository.Object, mapper, mockDefectsRepository.Object);
            //Act
            var result = await services.RegisterDefectAsync(registerDefectDto);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateDefect_CorrectObject_ReturnMapObject()
        {
            UpdateDefectDto updateDefectDto = new UpdateDefectDto()
            {
                Id = 1,
                Description = "Broken door",
                Status = Status.InRepair
            };
            Defect Defect = new Defect()
            {
                DefectId = 1,
                Description = "Door Finished",
                Status = Status.Finished
            };
            mockDefectsRepository
                .Setup(p => p.FindDefectByIdAsync(updateDefectDto.Id))
                .ReturnsAsync(new Defect()
                {
                    DefectId = 1,
                    Description = "Door Finished",
                    Status = Status.Finished
                });
            mockDefectsRepository
                .Setup(s => s.SaveChangesAsync())
                .Verifiable();
            var services = new DefectService(mockUsersRepository.Object, mockCarsRepository.Object, mapper, mockDefectsRepository.Object);
            //Act
            var result = await services.UpdateDefectAsync(updateDefectDto);
            //Assert
            var assertResult = Assert.IsType<DefectDto>(result);
            Assert.Equal(result.Description, updateDefectDto.Description);
            Assert.Equal(result.Status, updateDefectDto.Status);
        }

        [Fact]
        public async Task UpdateDefect_DefectNotFound_ReturnMapObject()
        {
            UpdateDefectDto updateDefectDto = new UpdateDefectDto()
            {
                Id = 1,
                Description = "Broken door",
                Status = Status.InRepair
            };
            mockDefectsRepository
                .Setup(p => p.FindDefectByIdAsync(updateDefectDto.Id))
                .ReturnsAsync(null as Defect);
            
            var services = new DefectService(mockUsersRepository.Object, mockCarsRepository.Object, mapper, mockDefectsRepository.Object);
            //Act
            var result = await services.UpdateDefectAsync(updateDefectDto);
            //Assert
            var assertResult = Assert.IsType<DefectDto>(result);
            Assert.Equal(result.DefectId, updateDefectDto.Id);
            Assert.Equal(result.Description, updateDefectDto.Description);
            Assert.Equal(result.Status, updateDefectDto.Status);
        }
    }
}
