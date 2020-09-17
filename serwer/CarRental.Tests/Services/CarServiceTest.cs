using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Models.Car;
using CarRental.Services.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Tests.Services
{
    public class CarServiceTest
    {
        private readonly Mock<ICarRepository> mockCarRepository;
        private readonly IMapper mapper;
        public CarServiceTest()
        {
            mockCarRepository = new Mock<ICarRepository>();
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<Car, CarDto>();
            });
            mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetAllCarsAsync_WhenCalled_ReturnsAllObjects()
        {
            //Arrange
            List<Car> cars = new List<Car>() { new Car(), new Car() };
            mockCarRepository
                .Setup(p => p.FindAllAsync())
                .ReturnsAsync(cars);
            var service = new CarService(mockCarRepository.Object, mapper);
            //Act
            var result = await service.GetAllCarsAsync();
            //Assert
            var assertResult = Assert.IsType<List<CarDto>>(result);
            Assert.Equal(cars.Count, assertResult.Count);
        }

        [Fact]
        public async Task GetCarByIdAsync_ExistingIdPassed_ReturnsCorrectObjects()
        {
            //Arrange
            int id = 1;
            mockCarRepository
                .Setup(p => p.FindByIdAsync(id))
                .ReturnsAsync(new Car() { CarId = id });
            var service = new CarService(mockCarRepository.Object, mapper);
            //Act
            var result = await service.GetCarByIdAsync(id);
            //Assert
            var assertResult = Assert.IsType<CarDto>(result);
            Assert.Equal(id, assertResult.CarId);
        }

        [Fact]
        public async Task GetCarByIdAsync_UnknownIdPassed_ReturnsNull()
        {
            //Arrange
            int id = 1;
            mockCarRepository
                .Setup(p => p.FindByIdAsync(id))
                .ReturnsAsync(null as Car);
            var service = new CarService(mockCarRepository.Object, mapper);
            //Act
            var result = await service.GetCarByIdAsync(id);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateCarAsync_PassedValidObject_CarCreated()
        {
            //Arrange
            Car car = new Car() { CarId = 1 };
            CarCreateDto carDto = new CarCreateDto();
            mockCarRepository
                .Setup(p => p.FindByIdAsync(0))
                .ReturnsAsync(car);
            mockCarRepository
                .Setup(s => s.SaveChangesAsync())
                .Verifiable();
            var service = new CarService(mockCarRepository.Object, mapper);
            //Act
            var result = await service.CreateCarAsync(carDto);
            //Assert
            Assert.Equal(result.CarId, car.CarId);
            Assert.IsType<CarDto>(result);
        }

        [Fact]
        public async Task UpdateCarAsync_PassedValidObject_ReturnsObject()
        {
            CarDto carDto = new CarDto() { CarId = 1, Brand = "KIA", Model = "Seed" };
            Car car = new Car() { CarId = 1, Brand = "KIHA", Model = "Sweet" };

            mockCarRepository
                .Setup(p => p.FindByIdAsync(carDto.CarId))
                .ReturnsAsync(car);
            mockCarRepository
               .Setup(s => s.SaveChangesAsync())
               .Verifiable();
            var service = new CarService(mockCarRepository.Object, mapper);
            //Act
            var result = await service.UpdateCarAsync(carDto);
            //Assert
            Assert.Equal(result.CarId, carDto.CarId);
            Assert.Equal(result.Brand, carDto.Brand);
            Assert.Equal(result.Model, carDto.Model);
            Assert.IsType<CarDto>(result);
        }
    }
}
