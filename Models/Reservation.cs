﻿// <copyright file="Reservation.cs" company="ZiedADJOUDJ">
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
        public string? CustomerName { get; set; }

        /// <summary>
        /// Gets the duration of the reservation.
        /// </summary>
        public int Duration
        {
            get
            {
                return DateOnly.FromDateTime(this.DateTo).DayNumber - DateOnly.FromDateTime(this.DateFrom).DayNumber;
            }
        }

        /// <summary>
        /// Function that tells if the reservation is compatible with an other.
        /// </summary>
        /// <param name="other">The reservation we want to check.</param>
        /// <returns>True if the 2 reservations are compatibles.</returns>
        public bool IsCompatible (Reservation other)
        {
            bool result = true;

            if (other == null)
            {
                return false;
            }

            if (other.IsValid())
            {
                if (this.DateFrom < other.DateFrom)
                {
                    // THIS reservation is BEFORE the OTHER reservation
                    if (DateOnly.FromDateTime(other.DateFrom).DayNumber - DateOnly.FromDateTime(this.DateFrom).DayNumber <= this.Duration)
                    {
                        result = false;
                    }
                }
                else if (this.DateFrom > other.DateFrom)
                {
                    // THIS reservation is AFTER the OTHER reservation
                    if (DateOnly.FromDateTime(this.DateFrom).DayNumber - DateOnly.FromDateTime(other.DateFrom).DayNumber <= other.Duration)
                    {
                        result = false;
                    }
                }
                else
                {
                    // In this case both reservations have same beggining dates --> impossible
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if this reservation is Valid.
        /// </summary>
        /// <returns>True if the reservation is valid.</returns>
        public bool IsValid()
        {
            bool result = true;

            if (this.DateFrom <= DateTime.Now)
            {
                result = false;
            }

            if (this.DateTo < this.DateFrom)
            {
                result = false;
            }

            if (DateOnly.FromDateTime(this.DateTo).DayNumber - DateOnly.FromDateTime(this.DateFrom).DayNumber > 3)
            {
                result = false;
            }

            if (DateOnly.FromDateTime(this.DateFrom).DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber > 30)
            {
                result = false;
            }

            return result;
        }
    }
}
