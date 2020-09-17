using CarRental.API.Attributes;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Location;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationsController : Controller
    {
        private readonly ILocationService locationService;
        public LocationsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        /// <summary>
        /// Get actual location of car by reservation id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns status 200 (Ok) with location or
        /// 204 (No content) if there is no saved location in the database. </returns>
        [HttpGet("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> GetLocationByReservationIdAsync(int id)
        {
            var location = await locationService.GetActualLocationByReservationIdAsync(id);
            return Ok(location);
        }

        /// <summary>
        /// Save new location into the database. Flag IsActual of old location is changed to false.
        /// </summary>
        /// <param name="locationDto"></param>
        /// <returns>Returns status 200 (Ok) with new location object.</returns>
        [HttpPost]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> CreateLocationAsync(LocationCreateDto locationDto)
        {
            var location = await locationService.CreateLocationAsync(locationDto);
            return Ok(location);
        }

        /// <summary>
        /// Change IsActual flag of last saved location to false. Location is found by reservations id.
        /// Method is available only for admin. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns status 200 (Ok) or
        /// 400 when there is no actual location for given reservation.</returns>
        [HttpDelete("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> DeleteLocationAsync(int id)
        {
            var location = await locationService.GetActualLocationByReservationIdAsync(id);
            if (location != null)
            {
                await locationService.DeleteLocationAsync(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
