using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;
        public CarService(
            ICarRepository carRepository,
            IMapper mapper)
        {
            this.carRepository = carRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Insert new car into database. 
        /// </summary>
        /// <param name="carDto"></param>
        /// <returns>Returns mapped car object - carDto.</returns>
        public async Task<CarDto> CreateCarAsync(CarCreateDto carDto)
        {
            Car car = new Car()
            {
                Brand = carDto.Brand,
                Model = carDto.Model,
                RegistrationNumber = carDto.RegistrationNumber,
                TypeOfCar = carDto.TypeOfCar,
                NumberOfDoor = carDto.NumberOfDoor,
                NumberOfSits = carDto.NumberOfSits,
                YearOfProduction = carDto.YearOfProduction,
                ImagePath = carDto.ImagePath,
                DateCreated = DateTime.Now
            };
            carRepository.Create(car);
            await carRepository.SaveChangesAsync();
            car = await carRepository.FindByIdAsync(car.CarId);
            return mapper.Map<CarDto>(car);
        }

        /// <summary>
        /// Change flag IsDeleted in the database from false to true.  
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteCarAsync(int id)
        {
            var car = await carRepository.FindByIdAsync(id);
            car.IsDeleted = true;
            await carRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Get all cars from database.
        /// </summary>
        /// <returns>Returns list with cars mapped to carDto. </returns>
        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        {
            var cars = await carRepository.FindAllAsync();
            return mapper.Map<IEnumerable<CarDto>>(cars);
        }

        /// <summary>
        /// Get car by given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns car mapped to carDto.</returns>
        public async Task<CarDto> GetCarByIdAsync(int id)
        {
            var car = await carRepository.FindByIdAsync(id);
            return mapper.Map<CarDto>(car);
        }

        /// <summary>
        /// Change one or many properties of car in the database.
        /// </summary>
        /// <param name="carDto"></param>
        /// <returns>Returns car mapped to carDto.</returns>
        public async Task<CarDto> UpdateCarAsync(CarDto carDto)
        {
            var car = await carRepository.FindByIdAsync(carDto.CarId);
            car.Update(carDto.Brand, carDto.Model, carDto.RegistrationNumber, carDto.TypeOfCar, carDto.NumberOfDoor,
            carDto.NumberOfSits, carDto.YearOfProduction, carDto.ImagePath);
            carRepository.Update(car);
            await carRepository.SaveChangesAsync();
            car = await carRepository.FindByIdAsync(carDto.CarId);
            return mapper.Map<CarDto>(car);
        }

        /// <summary>
        /// Get all available cars in given term.
        /// </summary>
        /// <param name="rentalDate"></param>
        /// <param name="returnDate"></param>
        /// <returns>Returns list of cars which are available.</returns>
        public async Task<IEnumerable<CarDto>> GetAvailableCars(DateTime rentalDate, DateTime returnDate)
        {
            var reservedCars = await carRepository.GetReservedCarsByDatesAsync(rentalDate, returnDate);
            var allCars = await carRepository.FindAllAsync();
            List<Car> availableCars = allCars.Except(reservedCars).ToList();
            return mapper.Map<IEnumerable<CarDto>>(availableCars);
        }
    }
}
