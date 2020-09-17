using CarRental.API.Attributes;
using CarRental.API.Resources;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Resources;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/terms")]
    [ApiController]
    [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
    public class TermsController : Controller
    {
        private readonly ITermService termService;
        private readonly ICarService carService;
        public ResourceManager resourcesManager;
        public TermsController(ITermService termService, ICarService carService)
        {
            this.termService = termService;
            this.carService = carService;
            resourcesManager = new ResourceManager("CarRental.API.Resources.ResourceFile", typeof(ResourceFile).Assembly);
        }

        /// <summary>
        /// Get list of days when choosen car can be rent. 
        /// Method can be called only with cars id and returns free days for next two weeks or
        /// can be called with cars id and the term we are interested in 
        /// (example: .../api/terms/5/2021-08-07/2021-09-07)
        /// and returns all free days from a week before given rental date to week after given return date.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rentalDate"></param>
        /// <param name="returnDate"></param>
        /// <returns>Returns status 200 (Ok) with list of dates or
        /// 204 (No content) when there is no free term or 
        /// 400 (Bad Request) if given informations are incorrect. </returns>
        [HttpGet("{id}")]
        [HttpGet("{id}/{rentalDate:datetime}/{returnDate:datetime}")]
        public async Task<IActionResult> GetFreeTermsByCarIdAsync(int id, DateTime? rentalDate, DateTime? returnDate)
        {
            if (!termService.DatesHaveValue(rentalDate, returnDate) || termService.DatesAreCorrect(rentalDate.Value, returnDate.Value))
            {
                var terms = await termService.GetFreeTermsByCarIdAsync(id, rentalDate, returnDate);
                return Ok(terms);
            }
            return BadRequest(resourcesManager.GetString("BadDateOrder"));
        }

        /// <summary>
        /// Get all available cars in given term.
        /// example: .../api/terms/2021-08-07/2021-09-07
        /// </summary>
        /// <param name="rentalDate"></param>
        /// <param name="returnDate"></param>
        /// <returns>Returns status 200 (Ok) with list of available cars or
        /// 204 (No content) when there is no free car in given term or 
        /// 400 (Bad Request) if given informations are incorrect. </returns>
        [HttpGet, Route("{rentalDate:datetime}/{returnDate:datetime}")]
        public async Task<IActionResult> GetAvailableCars(DateTime rentalDate, DateTime returnDate)
        {
            if (termService.DatesAreCorrect(rentalDate, returnDate))
            {
                var cars = await carService.GetAvailableCars(rentalDate, returnDate);
                return Ok(cars);
            }
            return BadRequest(resourcesManager.GetString("BadDateOrder"));
        }
    }
}