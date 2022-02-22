// <copyright file="ReservationContext.cs" company="ZiedADJOUDJ">
// Copyright (c) ZiedADJOUDJ. All rights reserved.
// </copyright>

namespace CancunHotelAPI.Models
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// ReservationContext database context.
    /// </summary>
    public class ReservationContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationContext"/> class.
        /// </summary>
        /// <param name="options">Options.</param>
        public ReservationContext(DbContextOptions<ReservationContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets reservations dbset.
        /// </summary>
        public DbSet<Reservation> Reservations { get; set; } = null!;
    }
}
