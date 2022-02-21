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
        public ReservationContext(DbContextOptions<ReservationContext> options)
            : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; } = null!;
    }
}
