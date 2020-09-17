using CarRental.API.Attributes;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationsController : Controller
    {
        private readonly IReservationService reservationService;
        public ReservationsController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        /// <summary>
        /// Get all reservations. Method is available only for admin. 
        /// </summary>
        /// <returns>Returns status 200 (Ok) and list of reservations or 
        /// 204 (No content) when there is no reservation in the database.</returns>
        [HttpGet]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            var reservations = await reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        /// <summary>
        /// Get reservation by given id. Method is available only for admin. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns status 200 (Ok) and reservations object or 
        /// 204 (No content) when there is no reservation in the database.</returns>
        [HttpGet("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var reservation = await reservationService.GetReservationByIdAsync(id);
            return Ok(reservation);
        }

        /// <summary>
        /// Get unfinished reservations by cars id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns status 200 (Ok) and list of reservations or 
        /// 204 (No content) when there is no reservation for given id in the database</returns>
        [HttpGet, Route("cars/{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> GetActualReservationsByCarIdAsync(int id)
        {
            var reservations = await reservationService.GetActualReservationsByCarIdAsync(id);
            return Ok(reservations);
        }

        /// <summary>
        /// Get unfinished reservations by users id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns status 200 (Ok) and list of reservations or 
        /// 204 (No content) when there is no reservation for given id in the database</returns>
        [HttpGet, Route("users/{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> GetAllReservationsByUserIdAsync(int id)
        {
            var reservations = await reservationService.GetAllReservationsByUserIdAsync(id);
            return Ok(reservations);
        }

        /// <summary>
        /// Insert new reservation into database. To create new one choosen car has to be available in given term.
        /// </summary>
        /// <param name="reservationCreateDto"></param>
        /// <returns>Returns status 200 (Ok) and new reservations object or
        /// 400 (Bad Request) if reservation can't be created in given term.</returns>
        [HttpPost]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> CreateReservationAsync(ReservationCreateDto reservationCreateDto)
        {
            if (await reservationService.ReservationCanBeCreatedAsync(reservationCreateDto))
            {
                var reservation = await reservationService.CreateReservationAsync(reservationCreateDto);
                return Ok(reservation);
            }
            return BadRequest();
        }

        /// <summary>
        /// Update reservation with given id. Term of new reservation can't collide with existing reservations.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reservationUpdateDto"></param>
        /// <returns>Returns status 200 (Ok) and new reservations object or
        /// 400 (Bad Request) if reservation can't be created in given term or there is no reservation with passed id.</returns>
        [HttpPatch("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> UpdateReservationAsync(int id, ReservationUpdateDto reservationUpdateDto)
        {
            if (id != reservationUpdateDto.ReservationId)
            {
                return BadRequest();
            }
            if (await reservationService.ReservationCanBeUpdatedAsync(reservationUpdateDto))
            {
                var reservation = await reservationService.UpdateReservationAsync(reservationUpdateDto);
                return Ok(reservation);
            }
            return BadRequest();
        }

        /// <summary>
        /// Change flag IsFinished from false to true for reservation with given id. Method is available only for admin. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns status 200 (Ok) or
        /// 400 (Bad Request) if there is no reservation with given id in the database.</returns>
        [HttpDelete("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> DeleteReservationAsync(int id)
        {
            var reservation = await reservationService.GetReservationByIdAsync(id);
            if (reservation != null)
            {
                await reservationService.DeleteReservationAsync(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}