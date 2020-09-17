using CarRental.API.Attributes;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Car;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : Controller
    {
        private readonly ICarService carService;
        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        /// <summary>
        /// Get all cars from database.
        /// </summary>
        /// <returns> Returns status 200 (Ok) and list of cars or 
        /// 204 (No content) when there is no car in the database.</returns>
        [HttpGet]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> GetAllCarsAsync()
        {
            var cars = await carService.GetAllCarsAsync();
            return Ok(cars);
        }

        /// <summary>
        /// Get car by its id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns status 200 (Ok) and car or 
        /// 204 (No content) when there is no car with given id in the database</returns>
        [HttpGet("{id}", Name = "GetById")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> GetCarByIdAsync(int id)
        {
            var car = await carService.GetCarByIdAsync(id);
            return Ok(car);
        }

        /// <summary>
        /// Insert new car into database. Method is available only for admin. 
        /// </summary>
        /// <param name="carDto"></param>
        /// <returns> Returns status 201 (created) with new car object or 
        /// 400 (bad request) if something went wrong.</returns>
        [HttpPost]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> CreateCarAsync(CarCreateDto carDto)
        {
            var car = await carService.CreateCarAsync(carDto);
            if (car == null)
            {
                return BadRequest();
            }
            return CreatedAtRoute(
                routeName: "GetById",
                routeValues: new { id = car.CarId },
                value: car);
        }

        /// <summary>
        /// Update car entity in the database. Method is available only for admin. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carDto"></param>
        /// <returns>Returns status 200 (Ok) with updated entity or 
        /// 204 (No content) if there is no car with given id in the database or
        /// 400 (Bad request) if given id is different than id in carDto</returns>
        [HttpPatch("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> UpdateCarAsync(int id, CarDto carDto)
        {
            if (id != carDto.CarId)
            {
                return BadRequest();
            }
            var result = await carService.UpdateCarAsync(carDto);
            return Ok(result);
        }

        /// <summary>
        /// Method changes flag IsDeleted in the database from false to true. 
        /// "Deleted" cars are no more shown by GetAllCarsById etc. Method is available only for admin. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns status 200 (Ok) or
        /// 400 (Bad Request) if there is no car with given id in the database or car was deleted before.</returns>
        [HttpDelete("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> DeleteCarAsync(int id)
        {
            var car = await carService.GetCarByIdAsync(id);
            if (car != null && car.IsDeleted != true)
            {
                await carService.DeleteCarAsync(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}