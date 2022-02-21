// <copyright file="ReservationsController.cs" company="ZiedADJOUDJ">
// Copyright (c) ZiedADJOUDJ. All rights reserved.
// </copyright>

#nullable disable

namespace CancunHotelAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CancunHotelAPI.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/Reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        /// <summary>
        /// Database context.
        /// </summary>
        private readonly ReservationContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsController"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public ReservationsController(ReservationContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// GET: api/Reservations.
        /// Method that get all reservations for the room.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await this.context.Reservations.ToListAsync();
        }

        /// <summary>
        /// GET: api/Reservations/5
        /// Mehod that returns a selected reservation.
        /// </summary>
        /// <param name="id">ID of the selected reservation.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(long id)
        {
            var reservation = await this.context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return this.NotFound();
            }

            return reservation;
        }

        /// <summary>
        /// PUT: api/Reservations/5
        /// Method that modify a reservation.
        /// Reminder : for a PUT method, we must input all properties of the reservation,
        /// For modifying just one property, see : <see cref="PatchReservation(long, Reservation)"/>.
        /// </summary>
        /// <param name="id">ID of the reservation.</param>
        /// <param name="reservation">Final reservation object.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(long id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return this.BadRequest();
            }

            this.context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ReservationExists(id))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.NoContent();
        }

        /// <summary>
        /// POST: api/Reservations
        /// Method that add a new Reservation
        /// Reminder :
        /// - You must book the room less than 3 days.
        /// - You can't book the room in 30 days or more.
        /// - You can't book the room for today.
        /// </summary>
        /// <param name="reservation">Reservation to add.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            this.context.Reservations.Add(reservation);
            await this.context.SaveChangesAsync();

            return this.CreatedAtAction(nameof(this.GetReservation), new { id = reservation.Id }, reservation);
        }


        /// <summary>
        /// DELETE: api/Reservations/5
        /// Method that delete a reservation.
        /// </summary>
        /// <param name="id">Id of the reservation we want to delete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(long id)
        {
            var reservation = await this.context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return this.NotFound();
            }

            this.context.Reservations.Remove(reservation);
            await this.context.SaveChangesAsync();

            return this.NoContent();
        }

        private bool ReservationExists(long id)
        {
            return this.context.Reservations.Any(e => e.Id == id);
        }
    }
}
