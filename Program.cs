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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllOrigins",
            builder =>
            {
                builder.WithOrigins("*");
            });
    });

var app = builder.Build();

// Swagger initialization
app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();