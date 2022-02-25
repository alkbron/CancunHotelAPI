// <copyright file="Program.cs" company="ZiedADJOUDJ">
// Copyright (c) ZiedADJOUDJ. All rights reserved.
// </copyright>

using CancunHotelAPI.Models;
using CancunHotelAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<ReservationsCancunDatabaseSettings>(
    builder.Configuration.GetSection("ReservationsCancunDatabase"));

builder.Services.AddSingleton<ReservationsService>();

// builder.Services.AddDbContext<ReservationContext>(opt => opt.UseInMemoryDatabase("ReservationList"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();