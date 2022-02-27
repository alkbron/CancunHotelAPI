// <copyright file="ReservationsCancunDatabaseSettings.cs" company="ZiedADJOUDJ">
// Copyright (c) ZiedADJOUDJ. All rights reserved.
// </copyright>

namespace CancunHotelAPI.Models
{
    /// <summary>
    /// Database settings for reservations.
    /// </summary>
    public class ReservationsCancunDatabaseSettings
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; } = null!;

        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        public string DatabaseName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the reservations collection name.
        /// </summary>
        public string ReservationsCollectionName { get; set; } = null!;
    }
}
