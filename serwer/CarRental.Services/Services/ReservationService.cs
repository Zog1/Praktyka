using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IMapper mapper;
        public ReservationService(IReservationRepository reservationRepository,
            IMapper mapper)
        {
            this.reservationRepository = reservationRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Insert new reservation into database. 
        /// </summary>
        /// <param name="reservationCreateDto"></param>
        /// <returns>Returns new reservations object mapped to reservationDto.</returns>
        public async Task<ReservationDto> CreateReservationAsync(ReservationCreateDto reservationCreateDto)
        {
            var reservation = new Reservation()
            {
                UserId = reservationCreateDto.UserId,
                CarId = reservationCreateDto.CarId,
                RentalDate = reservationCreateDto.RentalDate,
                ReturnDate = reservationCreateDto.ReturnDate,
                IsFinished = false,
                DateCreated = DateTime.Now
            };
            reservationRepository.Create(reservation);
            await reservationRepository.SaveChangesAsync();
            reservation = await reservationRepository.FindByIdAsync(reservation.ReservationId);
            return mapper.Map<ReservationDto>(reservation);
        }

        /// <summary>
        /// Changes the flag IsFinished of reservations object from false to true.
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteReservationAsync(int id)
        {
            await reservationRepository.DeleteAsync(id);
            await reservationRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Get all reservations with flag IsFinished set to false from the database.
        /// </summary>
        /// <returns>Returns list of reservations mapped to reservationDto.</returns>
        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            var reservations = await reservationRepository.FindAllAsync();
            return mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        /// <summary>
        /// Get reservation with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns reservations object mapped to reservationDto.</returns>
        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            var reservation = await reservationRepository.FindByIdAsync(id);
            return mapper.Map<ReservationDto>(reservation);
        }

        /// <summary>
        /// Change one or many properities of existing reservation in the database. 
        /// </summary>
        /// <param name="reservationUpdateDto"></param>
        /// <returns>Returns updated reservations object mapped to reservationDto.</returns>
        public async Task<ReservationDto> UpdateReservationAsync(ReservationUpdateDto reservationUpdateDto)
        {
            var reservation = await reservationRepository.FindByIdAsync(reservationUpdateDto.ReservationId);
            reservation.Update(reservationUpdateDto.RentalDate,
                reservationUpdateDto.ReturnDate,
                reservationUpdateDto.IsFinished);
            reservationRepository.Update(reservation);
            await reservationRepository.SaveChangesAsync();
            reservation = await reservationRepository.FindByIdAsync(reservationUpdateDto.ReservationId);
            return mapper.Map<ReservationDto>(reservation);
        }

        /// <summary>
        /// Check if reservation can be created. Method is being executed before CreateReservationAsync() method. 
        /// It checks how many reservations are in the database for given term and car. 
        /// If 0 then new reservation doesn't collide with other reservations.
        /// </summary>
        /// <param name="reservationDto"></param>
        /// <returns>Returns true if new reservation can be created and false if can't.</returns>
        public async Task<bool> ReservationCanBeCreatedAsync(ReservationCreateDto reservationDto)
        {
            var reservation = new Reservation()
            {
                RentalDate = reservationDto.RentalDate,
                ReturnDate = reservationDto.ReturnDate,
                CarId = reservationDto.CarId
            };
            List<Reservation> reservations = await reservationRepository.FilterReservationsAsync(reservation);
            return reservations.Count == 0 ? true : false;
        }

        /// <summary>
        /// Check if reservation can be updated. Method is being executed before UpdateReservationAsync() method. 
        /// It checks how many reservations are in the database for given term and car. 
        /// If 0 then new reservation doesn't collide with other reservations.
        /// If 1 then the method checks if found reservation has the same is as passed object.
        /// If ids are different than reservation can't be updated.
        /// </summary>
        /// <param name="reservationDto"></param>
        /// <returns>Returns true if new reservation can be created and false if can't.</returns>
        public async Task<bool> ReservationCanBeUpdatedAsync(ReservationUpdateDto reservationDto)
        {
            var reservation = new Reservation()
            {
                ReservationId = reservationDto.ReservationId,
                RentalDate = Convert.ToDateTime(reservationDto.RentalDate),
                ReturnDate = Convert.ToDateTime(reservationDto.ReturnDate),
                CarId = reservationDto.CarId
            };
            List<Reservation> reservations = await reservationRepository.FilterReservationsAsync(reservation);
            int count = reservations.Count;
            int id = count == 0 ? 0 : reservations.FirstOrDefault().ReservationId;
            return (count == 0 || (count == 1 && id == reservation.ReservationId)) ? true : false;
        }

        /// <summary>
        /// Get all reservations for given users id. It returns both finished and unfinished reservations.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns lisf of reservations found for users id mapped to reservationDto.</returns>
        public async Task<IEnumerable<ReservationDto>> GetAllReservationsByUserIdAsync(int id)
        {
            var reservations = await reservationRepository.FindAllByUserIdAsync(id);
            return mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        /// <summary>
        /// Get actual reservations for given cars id. Actual means that the flag IsFinished is set to false.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns lisf of reservations found for cars id mapped to reservationDto.</returns>
        public async Task<IEnumerable<ReservationDto>> GetActualReservationsByCarIdAsync(int id)
        {
            var reservations = await reservationRepository.FindAllByCarIdAsync(id);
            return mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }
    }
}
