// <copyright file="Reservation.cs" company="ZiedADJOUDJ">
// Copyright (c) Zied ADJOUDJ. All rights reserved.
// </copyright>

namespace CancunHotelAPI.Models
{
    /// <summary>
    /// Reservation Class.
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Reservation"/> class.
        /// </summary>
        public Reservation()
        {
        }

        /// <summary>
        /// Gets or sets the reservation Id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the beginnig date of the reservation.
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the ending date of the reservation.
        /// </summary>
        public DateTime DateTo { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string CustomerName { get; set; }
    }
}
