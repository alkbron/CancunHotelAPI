// <copyright file="ReservationsService.cs" company="ZiedADJOUDJ">
// Copyright (c) ZiedADJOUDJ. All rights reserved.
// </copyright>

namespace CancunHotelAPI.Services
{
    using CancunHotelAPI.Models;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

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

        public async Task<List<Reservation>> GetAsync() =>
            await this.reservationsCollection.Find(_ => true).ToListAsync();

        public async Task<Reservation?> GetAsync(string id) =>
            await this.reservationsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

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

        public async Task UpdateAsync(string id, Reservation updatedReservation)
        {
            if (!updatedReservation.IsValid())
            {
                throw new ArgumentException("Updated reservation not valid");
            }

            List<Reservation> reservations = await this.GetAsync();

            foreach (Reservation item in reservations)
            {
                if (!item.IsCompatible(updatedReservation))
                {
                    throw new ArgumentException("There's no disponibility at this time");
                }
            }

            await this.reservationsCollection.ReplaceOneAsync(x => x.Id == id, updatedReservation);
        }

        public async Task DeleteAsync(string id) =>
            await this.reservationsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
