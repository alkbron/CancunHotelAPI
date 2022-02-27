// <copyright file="ReservationsService.cs" company="ZiedADJOUDJ">
// Copyright (c) ZiedADJOUDJ. All rights reserved.
// </copyright>

namespace CancunHotelAPI.Services
{
    using CancunHotelAPI.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    /// <summary>
    /// Reservation service.
    /// </summary>
    public class ReservationsService
    {
        /// <summary>
        /// The reservations collection.
        /// </summary>
        private readonly IMongoCollection<Reservation> reservationsCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsService"/> class.
        /// </summary>
        /// <param name="reservationsCancunDatabaseSettings">Settings.</param>
        public ReservationsService(
            IOptions<ReservationsCancunDatabaseSettings> reservationsCancunDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(
                reservationsCancunDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                reservationsCancunDatabaseSettings.Value.DatabaseName);

            this.reservationsCollection = mongoDatabase.GetCollection<Reservation>(
                reservationsCancunDatabaseSettings.Value.ReservationsCollectionName);
        }

        /// <summary>
        /// Get all reservations from DB.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<List<Reservation>> GetAsync() =>
            await this.reservationsCollection.Find(_ => true).ToListAsync();

        /// <summary>
        /// Gets one reservation from DB.
        /// </summary>
        /// <param name="id">ID of the reservation.</param>
        /// <returns>The reservation chosen.</returns>
        public async Task<Reservation?> GetAsync(string id) =>
            await this.reservationsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        /// <summary>
        /// Inserts new reservation in DB.
        /// </summary>
        /// <param name="newReservation">The new reservation.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">If the reservation is not valid.</exception>
        public async Task CreateAsync(Reservation newReservation)
        {
            if (!newReservation.IsValid())
            {
                throw new ArgumentException("Reservation not valid");
            }

            List<Reservation> reservations = await this.GetAsync();

            foreach (Reservation item in reservations)
            {
                if (!item.IsCompatible(newReservation))
                {
                    throw new ArgumentException("There's no disponibility at this time");
                }
            }

            await this.reservationsCollection.InsertOneAsync(newReservation);
        }

        /// <summary>
        /// Updates reservation in database.
        /// </summary>
        /// <param name="id">ID of the reservation we want to update.</param>
        /// <param name="updatedReservation">The new informations of the reservation.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task UpdateAsync(string id, Reservation updatedReservation)
        {
            if (!updatedReservation.IsValid())
            {
                throw new ArgumentException("Updated reservation not valid");
            }

            List<Reservation> reservations = await this.GetAsync();

            foreach (Reservation item in reservations)
            {
                if (updatedReservation.Id != item.Id && !item.IsCompatible(updatedReservation))
                {
                    throw new ArgumentException("There's no disponibility at this time");
                }
            }

            await this.reservationsCollection.ReplaceOneAsync(x => x.Id == id, updatedReservation);
        }

        /// <summary>
        /// Deletes a reservation in DB.
        /// </summary>
        /// <param name="id">ID of the reservation we want to delete.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task DeleteAsync(string id) =>
            await this.reservationsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
