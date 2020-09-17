using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Models.Reservation;
using CarRental.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Tests.Services
{
    public class ReservationServiceTest
    {
        private readonly Mock<IReservationRepository> mockReservationRepository;
        private readonly IMapper mapper;
        public ReservationServiceTest()
        {
            mockReservationRepository = new Mock<IReservationRepository>();
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<Reservation, ReservationDto>();
            });
            mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetAllReservationsAsync_WhenCalled_ReturnsAllObjects()
        {
            //Arrange
            List<Reservation> reservations = new List<Reservation>() { new Reservation(), new Reservation() };
            mockReservationRepository
                .Setup(p => p.FindAllAsync())
                .ReturnsAsync(reservations);
            var service = new ReservationService(mockReservationRepository.Object, mapper);
            //Act
            var result = await service.GetAllReservationsAsync();
            //Assert
            var assertResult = Assert.IsType<List<ReservationDto>>(result);
            Assert.Equal(reservations.Count, assertResult.Count);
        }

        [Fact]
        public async Task GetReservationByIdAsync_ExistingIdPassed_ReturnsCorrectObjects()
        {
            //Arrange
            int id = 1;
            mockReservationRepository
                .Setup(p => p.FindByIdAsync(id))
                .ReturnsAsync(new Reservation() { ReservationId = id });
            var service = new ReservationService(mockReservationRepository.Object, mapper);
            //Act
            var result = await service.GetReservationByIdAsync(id);
            //Assert
            var assertResult = Assert.IsType<ReservationDto>(result);
            Assert.Equal(id, assertResult.ReservationId);
        }

        [Fact]
        public async Task GetReservationByIdAsync_UnexistingIdPassed_ReturnsCorrectObjects()
        {
            //Arrange
            int id = 1;
            mockReservationRepository
                .Setup(p => p.FindByIdAsync(id))
                .ReturnsAsync(null as Reservation);
            var service = new ReservationService(mockReservationRepository.Object, mapper);
            //Act
            var result = await service.GetReservationByIdAsync(id);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateReservationAsync_ValidObjectPassed_CreatedObject()
        {
            Reservation reservation = new Reservation() { ReservationId = 1 };
            ReservationCreateDto reservationDto = new ReservationCreateDto();
            mockReservationRepository
                .Setup(p => p.FindByIdAsync(0))
                .ReturnsAsync(reservation);
            mockReservationRepository
               .Setup(s => s.SaveChangesAsync())
               .Verifiable();
            var service = new ReservationService(mockReservationRepository.Object, mapper);
            //Act
            var result = await service.CreateReservationAsync(reservationDto);
            //Assert
            Assert.Equal(result.ReservationId, reservation.ReservationId);
            Assert.IsType<ReservationDto>(result);
        }

        [Fact]
        public async Task UpdateCarAsync_PassedValidObject_ReturnsObject()
        {
            //Arrange
            ReservationUpdateDto reservationDto = new ReservationUpdateDto()
            {
                ReservationId = 1,
                RentalDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(5),
                IsFinished = true
            };
            Reservation reservation = new Reservation()
            {
                ReservationId = 1,
                RentalDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(10),
                IsFinished = false
            };

            mockReservationRepository
                .Setup(p => p.FindByIdAsync(reservationDto.ReservationId))
                .ReturnsAsync(reservation);
            mockReservationRepository
                .Setup(s => s.SaveChangesAsync())
                .Verifiable();
            var service = new ReservationService(mockReservationRepository.Object, mapper);
            //Act
            var result = await service.UpdateReservationAsync(reservationDto);
            //Assert
            Assert.Equal(result.ReservationId, reservationDto.ReservationId);
            Assert.Equal(Convert.ToDateTime(result.RentalDate), reservationDto.RentalDate);
            Assert.Equal(Convert.ToDateTime(result.ReturnDate), reservationDto.ReturnDate);
            Assert.Equal(result.IsFinished, reservationDto.IsFinished);
            Assert.IsType<ReservationDto>(result);
        }

        [Fact]
        public async Task ReservationCanBeCreatedAsync_CarIsAvailable_ReturnsTrue()
        {
            //Arrange
            var reservationDto = new ReservationCreateDto() { };
            mockReservationRepository
                .Setup(p => p.FilterReservationsAsync(It.IsAny<Reservation>()))
                .ReturnsAsync(new List<Reservation>() { });
            var service = new ReservationService(mockReservationRepository.Object, mapper);
            //Act
            var result = await service.ReservationCanBeCreatedAsync(reservationDto);
            //Assert
            Assert.True(result);

        }

        [Fact]
        public async Task ReservationCanBeCreatedAsync_CarIsNotAvailable_ReturnsFalse()
        {
            //Arrange
            var reservationDto = new ReservationCreateDto() { };
            var reservations = new List<Reservation>() { new Reservation() };
            mockReservationRepository
                .Setup(p => p.FilterReservationsAsync(It.IsAny<Reservation>()))
                .ReturnsAsync(reservations);
            var service = new ReservationService(mockReservationRepository.Object, mapper);
            //Act
            var result = await service.ReservationCanBeCreatedAsync(reservationDto);
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ReservationCanBeUpdatedAsync_CarIsAvailable_ReturnsTrue()
        {
            //Arrange
            var reservationDto = new ReservationUpdateDto() { };
            var reservations = new List<Reservation>() { };
            mockReservationRepository
                .Setup(p => p.FilterReservationsAsync(It.IsAny<Reservation>()))
                .ReturnsAsync(reservations);
            var service = new ReservationService(mockReservationRepository.Object, mapper);
            //Act
            var result = await service.ReservationCanBeUpdatedAsync(reservationDto);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ReservationCanBeUpdatedAsync_ReservationCanBeExtended_ReturnsTrue()
        {
            //Arrange
            var reservationDto = new ReservationUpdateDto() { ReservationId = 1 };
            var reservations = new List<Reservation>() { new Reservation() { ReservationId = 1 } };
            mockReservationRepository
                .Setup(p => p.FilterReservationsAsync(It.IsAny<Reservation>()))
                .ReturnsAsync(reservations);
            var service = new ReservationService(mockReservationRepository.Object, mapper);
            //Act
            var result = await service.ReservationCanBeUpdatedAsync(reservationDto);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ReservationCanBeUpdatedAsync_CarIsNotAvailable_ReturnsFalse()
        {
            //Arrange
            var reservationDto = new ReservationUpdateDto() { };
            var reservations = new List<Reservation>() { new Reservation(), new Reservation() };
            mockReservationRepository
                .Setup(p => p.FilterReservationsAsync(It.IsAny<Reservation>()))
                .ReturnsAsync(reservations);
            var service = new ReservationService(mockReservationRepository.Object, mapper);
            //Act
            var result = await service.ReservationCanBeUpdatedAsync(reservationDto);
            //Assert
            Assert.False(result);
        }
    }
}
