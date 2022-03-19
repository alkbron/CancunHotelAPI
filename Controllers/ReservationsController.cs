// <copyright file="ReservationsController.cs" company="ZiedADJOUDJ">
// Copyright (c) ZiedADJOUDJ. All rights reserved.
// </copyright>

namespace CancunHotelAPI.Controllers
{
    using CancunHotelAPI.Models;
    using CancunHotelAPI.Services;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Bson;

    /// <summary>
    /// Controller class.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsService reservationsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsController"/> class.
        /// </summary>
        /// <param name="reservationsService">Reservations service.</param>
        public ReservationsController(ReservationsService reservationsService) =>
            this.reservationsService = reservationsService;

        /// <summary>
        /// Function GET, to get all the current reservations of the room.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        public async Task<List<Reservation>> Get() =>
            await this.reservationsService.GetAsync();


        [HttpGet]
        public async Task<List<string>> GetAllDaysReserved() =>
            await this.reservationsService.GetAsyncAllDaysReserved();

            
        /// <summary>
        /// Function GET, to get all informations about one reservation.
        /// </summary>
        /// <param name="id">The ID of the chosen reservation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Reservation>> Get(string id)
        {
            Reservation? reservation = await this.reservationsService.GetAsync(id);

            if (reservation is null)
            {
                return this.NotFound();
            }

            return this.Ok(reservation);
        }

        /// <summary>
        /// POST function to book the room.
        /// </summary>
        /// <param name="newReservation">The booking details.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        public async Task<ActionResult> Post(Reservation newReservation)
        {
            try
            {
                newReservation.Id = ObjectId.GenerateNewId().ToString();
                await this.reservationsService.CreateAsync(newReservation);

                return this.CreatedAtAction(nameof(this.Get), new { id = newReservation.Id }, newReservation);
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// PUT Function to update a reservation.
        /// </summary>
        /// <param name="id">The id of the reservation we want to update.</param>
        /// <param name="updatedReservation">the updated informations.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Reservation updatedReservation)
        {
            try
            {
                Reservation? reservation = await this.reservationsService.GetAsync(id);

                if (reservation is null)
                {
                    return this.NotFound();
                }

                updatedReservation.Id = reservation.Id;

                await this.reservationsService.UpdateAsync(id, updatedReservation);

                return this.NoContent();
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// DELETE function for canceling a reservation.
        /// </summary>
        /// <param name="id">ID of the reservation we want to cancel.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            Reservation? reservation = await this.reservationsService.GetAsync(id);

            if (reservation is null)
            {
                return this.NotFound();
            }

            await this.reservationsService.DeleteAsync(id);

            return this.NoContent();
        }
    }
}
