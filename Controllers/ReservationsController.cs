// <copyright file="NewReservationsController.cs" company="ZiedADJOUDJ">
// Copyright (c) ZiedADJOUDJ. All rights reserved.
// </copyright>

namespace CancunHotelAPI.Controllers
{
    using CancunHotelAPI.Models;
    using CancunHotelAPI.Services;
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

        public ReservationsController(ReservationsService reservationsService) =>
            this.reservationsService = reservationsService;

        [HttpGet]
        public async Task<List<Reservation>> Get() =>
            await this.reservationsService.GetAsync();


        [HttpGet]
        public async Task<List<string>> GetAllDaysReserved() =>
            await this.reservationsService.GetAsyncAllDaysReserved();

            
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
